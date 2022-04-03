using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace SADA.Web.Areas.Client.Controllers;

[Area("Client")]
public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWorks;

    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWorks = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productsList = _unitOfWorks.Product.GetAll(
            includeProperties: "Category", 
            orderBy: p => p.Id, orderByDirection: SD.Descending
            );
        return View(productsList);  
    }

    [HttpGet]
    public IActionResult Details(int productId)
    {
        ShoppingCart obj = new()
        {
            Count = 1,
            ProductId = productId,
            UserId = HttpContext.Session.GetObject<ApplicationUser>(SD.SessionLoggedUser).Id,
        Product = _unitOfWorks.Product.GetFirstOrDefault(o => o.Id== productId, includeProperties: "Category")
        };

        return View(obj);
    }

    [HttpPost, ValidateAntiForgeryToken, Authorize] //only logged user can do it
    public IActionResult Details(ShoppingCart obj)
    {
        if (!ModelState.IsValid)
            return View(obj);
        
        var cart = _unitOfWorks.ShoppingCart.GetFirstOrDefault(criteria:
            p => p.ProductId == obj.ProductId
            && p.Color == obj.Color && p.Size == obj.Size
        );

        if (cart is null)
        {
            //added for first time
            _unitOfWorks.ShoppingCart.Add(obj);
            _unitOfWorks.Save();
            //update session value for cart counts
            HttpContext.Session.IncrementValue(SD.SessionCart, 1);
        }
        else
        {
            _unitOfWorks.ShoppingCart.IncrementCount(cart, obj.Count);
            _unitOfWorks.Save();
        }

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}