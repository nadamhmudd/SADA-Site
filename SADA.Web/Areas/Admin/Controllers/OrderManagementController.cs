using Microsoft.AspNetCore.Mvc;
using SADA.Core.Interfaces;
using SADA.Core.Models;
using SADA.Service;

namespace SADA.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWorks;
        public OrderManagementController(IUnitOfWork unitOfWork)
        {
            _unitOfWorks = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader>? orderslist= null;
            if (String.IsNullOrWhiteSpace(status) || status == "all")
            {
                orderslist = _unitOfWorks.OrderHeader.GetAll("ApplicationUser", o => o.Id, SD.Descending);
            }
            else if (status == SD.Status[0])
            {
                orderslist = _unitOfWorks.OrderHeader.GetAll("ApplicationUser", o => o.Id, SD.Descending,
                    criteria: o => o.PaymentStatus == status
                    ); 
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
}
