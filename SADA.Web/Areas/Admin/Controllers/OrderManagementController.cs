using Stripe;

namespace SADA.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class OrderManagementController : Controller
{
    #region Props
    private readonly IUnitOfWork _unitOfWorks;

    [BindProperty]
    public OrderManagementVM OrderManagementVM { get; set; }
    #endregion

    #region Constructor(s)
    public OrderManagementController(IUnitOfWork unitOfWork)
    {
        _unitOfWorks = unitOfWork;
    }
    #endregion

    #region Actions
    public IActionResult Index() => View();

    public IActionResult Details(int orderId)
    {
        OrderManagementVM = new OrderManagementVM()
        {
            OrderHeader = _unitOfWorks.OrderHeader.GetFirstOrDefault(o => o.Id == orderId, "ApplicationUser"),
            OrderDetails = new List<OrderDetail>()
        };
        foreach (var order in OrderManagementVM.OrderHeader.Items)
        {
            order.Product = _unitOfWorks.Product.GetById(order.ProductId);
        }

        return View(OrderManagementVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateOrderDetails()
    {
        var orderHeaderFromD =_unitOfWorks.OrderHeader.GetFirstOrDefault(o => o.Id == OrderManagementVM.OrderHeader.Id, tracked:false);
        orderHeaderFromD.Name = OrderManagementVM.OrderHeader.Name;
        orderHeaderFromD.PhoneNumber = OrderManagementVM.OrderHeader.PhoneNumber;
        orderHeaderFromD.StreetAddress = OrderManagementVM.OrderHeader.StreetAddress;
        orderHeaderFromD.City = OrderManagementVM.OrderHeader.City;
        if (OrderManagementVM.OrderHeader.Carrier != null)
        {
            orderHeaderFromD.Carrier = OrderManagementVM.OrderHeader.Carrier;
        }
        _unitOfWorks.OrderHeader.Update(orderHeaderFromD);
        _unitOfWorks.Save();

        TempData["Success"] = "Order Details Updated Successfully.";
        return RedirectToAction("Details", "OrderManagement", new { orderId = OrderManagementVM.OrderHeader.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StartProcessing()
    {
        _unitOfWorks.OrderHeader.UpdateStatus(OrderManagementVM.OrderHeader.Id, SD.Status.Processing.ToString());
        _unitOfWorks.Save();

        TempData["Success"] = "Order Status Updated Successfully.";
        return RedirectToAction("Details", "OrderManagement", new { orderId = OrderManagementVM.OrderHeader.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ShipOrder()
    {
        var orderHeaderFromD = _unitOfWorks.OrderHeader.GetFirstOrDefault(o => o.Id == OrderManagementVM.OrderHeader.Id, tracked:false);
        orderHeaderFromD.Carrier = OrderManagementVM.OrderHeader.Carrier;
        orderHeaderFromD.ShippingDate = DateTime.Now;
        orderHeaderFromD.OrderStatus = SD.Status.Shipped.ToString();
        
        _unitOfWorks.OrderHeader.Update(orderHeaderFromD);
        _unitOfWorks.Save();

        TempData["Success"] = "Order Shipped Successfully.";
        return RedirectToAction("Details", "OrderManagement", new { orderId = OrderManagementVM.OrderHeader.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeliveredOrder()
    {
        _unitOfWorks.OrderHeader.UpdateStatus(OrderManagementVM.OrderHeader.Id, SD.Status.Delivered.ToString());
        _unitOfWorks.Save();

        TempData["Success"] = "Order Delivered Successfully.";
        return RedirectToAction("Details", "OrderManagement", new { orderId = OrderManagementVM.OrderHeader.Id });
    }

    public IActionResult CancelOrRefundOrder(SD.Status status = SD.Status.Cancelled)
    {
        var orderHeader = _unitOfWorks.OrderHeader.GetFirstOrDefault(u => u.Id == OrderManagementVM.OrderHeader.Id, tracked:false);
        if (orderHeader.PaymentStatus == SD.Status.Approved.ToString())
        {
            var options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId
            };

            var service = new RefundService();
            Refund refund = service.Create(options);

            _unitOfWorks.OrderHeader.UpdateStatus(orderHeader.Id, status.ToString(), SD.Status.Refunded.ToString());
        }
        else
        {
            _unitOfWorks.OrderHeader.UpdateStatus(orderHeader.Id, status.ToString(), status.ToString());
        }
        _unitOfWorks.Save();
        
        if(status == SD.Status.Cancelled)
            TempData["Success"] = "Order Cancelled Successfully.";
        else
            TempData["Success"] = "Order Refunded Successfully.";


        return RedirectToAction("Details", "OrderManagement", new { orderId = OrderManagementVM.OrderHeader.Id });
    }
    #endregion

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll(string status)
    {
        IEnumerable<OrderHeader>? orderslist= null;
        if (String.IsNullOrWhiteSpace(status) || status == "all")
        {
            orderslist = _unitOfWorks.OrderHeader.GetAll("ApplicationUser", o => o.Id, SD.Descending);
        }
        else
        {
            orderslist = _unitOfWorks.OrderHeader.GetAll("ApplicationUser", o => o.Id, SD.Descending,
                criteria: o => o.OrderStatus == status
                ); 
        }

        return Json(new { data = orderslist });
    }
    #endregion
}