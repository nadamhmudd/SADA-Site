using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe.Checkout;

namespace SADA.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWorks;
        private readonly ISmsSender _SmsSender;
        private readonly ApplicationUser _loggedUser;
        private readonly PaymentController _paymentController;

        public CartController(IUnitOfWork unitOfWork,
            ISmsSender smsSender,
            IHttpContextAccessor HttpContextAccessor,
            IURLHelper urlHelper)
        {
            _unitOfWorks = unitOfWork;
            _SmsSender = smsSender;
            _loggedUser = HttpContextAccessor.HttpContext.Session.GetObject<ApplicationUser>(SD.SessionLoggedUser);
            _paymentController = new PaymentController(urlHelper);
        }

        [BindProperty] //for post method
        public ShoppingCartVM ShoppingCartVM { get; set; }

        //---------------------------------- Methods---------------------------------------------------
        [HttpGet]
        public IActionResult Index()
        {
            ShoppingCartVM = UploadCartFromDb();
            return View(ShoppingCartVM);
        }

        [HttpGet]
        public IActionResult Summary()
        {
            ShoppingCartVM = UploadCartFromDb();
            ShoppingCartVM.OrderHeader.Name = _loggedUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = _loggedUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = _loggedUser.StreetAddress;

            ShoppingCartVM.PaymentMethod = _unitOfWorks.PaymentMethod.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
            
            ShoppingCartVM.Governorates = _unitOfWorks.Governorate.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
            
            ShoppingCartVM.Cities = _unitOfWorks.City.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });

            return View(ShoppingCartVM);
        }

        [HttpPost, ActionName("Summary"), ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            //OrderHeader
            ShoppingCartVM.OrderHeader.ApplicationUserId = _loggedUser.Id;
            
            ShoppingCartVM.OrderHeader.OrderStatus = SD.Status.Pending.ToString();
            
            ShoppingCartVM.OrderHeader.PaymentStatus = SD.Status.Pending.ToString();

            //OrderDetail
            ShoppingCartVM.OrderHeader.Items = new List<OrderDetail>();
            ShoppingCartVM.ListCart = CollectOrderItems();
            foreach (var item in ShoppingCartVM.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = item.ProductId,
                    Count = item.Count,
                    Price = item.Product.Price
                };
                ShoppingCartVM.OrderHeader.Items.Add(orderDetail);
            }

            _unitOfWorks.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWorks.Save();

            if (ShoppingCartVM.OrderHeader.PaymentMethodId == SD.PaymentByCardID)
            {
                //stripe setting
                Session session = CheckoutByStripe(ShoppingCartVM.ListCart);
                _unitOfWorks.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWorks.Save();
                
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            else if (ShoppingCartVM.OrderHeader.PaymentMethodId == SD.PaymentByCashID)
            {
                return RedirectToAction("OrderConfirmation", new { id = ShoppingCartVM.OrderHeader.Id });
                //OrderConfirmationAsync(ShoppingCartVM.OrderHeader.Id);
            }

            return View();
        }

        public async Task<IActionResult> OrderConfirmationAsync(int id)
        {
            OrderHeader orderHeader = _unitOfWorks.OrderHeader.GetById(id);
            if(orderHeader.PaymentMethodId == SD.PaymentByCardID)
            {
                //check the stripe status
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                if (session.PaymentStatus.ToLower() != "paid")
                {
                    return RedirectToAction("Summary"); 
                }
            }

            _unitOfWorks.OrderHeader.UpdateStatus(id, SD.Status.Approved.ToString(), SD.Status.Approved.ToString());
            _unitOfWorks.Save();

            //remove shopping cart
            List<ShoppingCart> ListCart = _unitOfWorks.ShoppingCart.GetAll(
                includeProperties: "Product",
                criteria: c => c.UserId == orderHeader.ApplicationUserId
                ).ToList();

            _unitOfWorks.ShoppingCart.RemoveRange(ListCart);
            _unitOfWorks.Save();

            //clear session value for cart
            HttpContext.Session.Remove(SD.SessionCart);

            //send sms message
            await _SmsSender.SendSMSAsync(orderHeader.PhoneNumber, $"Order Placed on SADA Suits. Your OrderID:{orderHeader.Id}");

            return View(id);
        }
        
        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWorks.ShoppingCart.GetById(cartId);
            
            _unitOfWorks.ShoppingCart.IncrementCount(cartFromDb, 1);
            
            _unitOfWorks.Save();

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWorks.ShoppingCart.GetById(cartId);
            if (cartFromDb.Count > 1)
            {
                _unitOfWorks.ShoppingCart.DecrementCount(cartFromDb, 1);
            }
            else //delete
            {
                _unitOfWorks.ShoppingCart.Remove(cartFromDb);
                HttpContext.Session.DecrementValue(SD.SessionCart, 1);
            }
            _unitOfWorks.Save();

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWorks.ShoppingCart.GetById(cartId);
            
            _unitOfWorks.ShoppingCart.Remove(cartFromDb);
            
            _unitOfWorks.Save();

            HttpContext.Session.DecrementValue(SD.SessionCart, 1);

            return RedirectToAction(nameof(Index));
        }

        //----------------------------------Helper Methods-------------------------------------------------
        private ShoppingCartVM UploadCartFromDb()
        {
            //get cart
            ShoppingCartVM ShoppingCartVM = new()
            {
                ListCart = CollectOrderItems(),
                OrderHeader = new()
            };
            //calculate total
            foreach (var item in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += (item.Price * item.Count);
            }

            return ShoppingCartVM;
        }

        private IEnumerable<ShoppingCart> CollectOrderItems()
        {
            return _unitOfWorks.ShoppingCart.GetAll(
                includeProperties: "Product",
                criteria: c => c.UserId == _loggedUser.Id );
        }
        
        private Session CheckoutByStripe(IEnumerable<ShoppingCart> CartList)
        {
            var options = _paymentController.Stripe(
                            successUrl: $"client/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                            cancelUrl: "client/cart/index");

            foreach (var item in CartList)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)item.Price * 100,
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        },

                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(sessionLineItem);
            }

            return new SessionService().Create(options);
        }
    }
}