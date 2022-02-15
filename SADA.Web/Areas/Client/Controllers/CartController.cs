using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SADA.Core.Interfaces;
using SADA.Core.ViewModels;
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

        [BindProperty]
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
            };

            //calculate total
            foreach( var item in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.CartTotal += (item.Product.Price * item.Count);
            }

            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            return View();
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
