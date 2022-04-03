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
            var user = HttpContext.Session.GetObject<ApplicationUser>(SD.SessionLoggedUser);

            if(user != null)
            {
                if(HttpContext.Session.GetInt32(SD.SessionCart) == null)
                {
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(
                            criteria: c => c.UserId == user.Id).ToList().Count);
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
