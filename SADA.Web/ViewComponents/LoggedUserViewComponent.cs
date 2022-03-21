using System.Security.Claims;

namespace SADA.Web.ViewComponents
{
    public class LoggedUserViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoggedUserViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //retrieve logged user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            //create session for logged user
            HttpContext.Session.SetObject(SD.SessionLoggedUser,
                _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value));

            return View();
        }
    }
}
