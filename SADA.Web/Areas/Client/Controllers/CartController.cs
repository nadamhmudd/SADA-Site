using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SADA.Core.Interfaces;
using SADA.Core.Models;
using SADA.Core.ViewModels;
using SADA.Service;
using System.Security.Claims;

namespace SADA.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfworks;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfworks = unitOfWork;
        }

        [BindProperty] //for post method
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public IActionResult Index()
        {
            //find user to get his cart
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //get cart
            ShoppingCartVM = new()
            {
                ListCart = _unitOfworks.ShoppingCart.GetAll(
                includeProperties: "Product",
                criteria: c => c.ApplicationUserID == userId
                ),
                OrderHeader = new()
            };

            //calculate total
            foreach( var item in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
            }

            return View(ShoppingCartVM);
        }

        [HttpGet]
        public IActionResult Summary()
        {
            //find user to get his cart
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //get cart
            ShoppingCartVM = new()
            {
                ListCart = _unitOfworks.ShoppingCart.GetAll(
                includeProperties: "Product",
                criteria: c => c.ApplicationUserID == userId
                ),
                OrderHeader = new()
                {
                    //Initialise order header
                    ApplicationUserId = userId,
                    ApplicationUser = _unitOfworks.ApplicationUser.GetFirstOrDefault(u => u.Id == userId),
                }
            };
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;

            foreach (var item in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            //OrderHeader
            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            ShoppingCartVM.OrderHeader.PaymentOption = SD.PaymentOptions.Cash;
            ShoppingCartVM.OrderHeader.PaymentStatus = SD.StatusPending;
            _unitOfworks.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfworks.Save();

            //OrderDetail
            ShoppingCartVM.ListCart = _unitOfworks.ShoppingCart.GetAll(
                includeProperties: "Product",
                criteria: c => c.ApplicationUserID == ShoppingCartVM.OrderHeader.ApplicationUserId
                );
            foreach (var item in ShoppingCartVM.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    ProductId = item.ProductID,
                    Count = item.Count,
                    Price = item.Product.Price
                };
                _unitOfworks.OrderDetail.Add(orderDetail);
                _unitOfworks.Save();
            }

            //remove shopping carty
            _unitOfworks.ShoppingCart.RemoveRange(ShoppingCartVM.ListCart);
            _unitOfworks.Save();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfworks.ShoppingCart.GetById(cartId);
            _unitOfworks.ShoppingCart.IncrementCount(cartFromDb, 1);
            _unitOfworks.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfworks.ShoppingCart.GetById(cartId);
            if(cartFromDb.Count > 1)
            {
                _unitOfworks.ShoppingCart.DecrementCount(cartFromDb, 1);
            }
            else //delete
            {
                _unitOfworks.ShoppingCart.Remove(cartFromDb);
            }
            _unitOfworks.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfworks.ShoppingCart.GetById(cartId);
            _unitOfworks.ShoppingCart.Remove(cartFromDb);
            _unitOfworks.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
