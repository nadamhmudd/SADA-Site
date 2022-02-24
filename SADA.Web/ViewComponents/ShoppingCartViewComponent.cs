using Microsoft.AspNetCore.Mvc;
using SADA.Core.Interfaces;
using SADA.Service;
using System.Security.Claims;

namespace SADA.Web.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //get logged user
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var user = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(user != null)
            {
                if(HttpContext.Session.GetInt32(SD.SessionCart) == null)
                {
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(criteria: c => c.ApplicationUserID == user.Value).ToList().Count);
                }
                return View(HttpContext.Session.GetInt32(SD.SessionCart));
            }
            else
            {
                return View(0);
            }
        }
    }
}
