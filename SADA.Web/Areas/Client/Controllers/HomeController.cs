using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SADA.Core.Interfaces;
using SADA.Core.Models;
using SADA.Service;
using System.Diagnostics;
using System.Security.Claims;

namespace SADA.Web.Areas.Client.Controllers;

[Area("Client")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWorks;
    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
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
            ProductID = productId,
            Product = _unitOfWorks.Product.GetFirstOrDefault(o => o.Id== productId, includeProperties: "Category")
        };

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize] //only logged user can do it
    public IActionResult Details(ShoppingCart obj)
    {
        //retrieve application user id
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        obj.ApplicationUserID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value; //get user id

        ShoppingCart cartFromDb = _unitOfWorks.ShoppingCart.GetFirstOrDefault(criteria: 
               u => u.ApplicationUserID == obj.ApplicationUserID && u.ProductID == obj.ProductID
            );

        if(cartFromDb is null)
        {
            //added first time
            _unitOfWorks.ShoppingCart.Add(obj);
            //update session value for cart counts
            HttpContext.Session.IncrementValue(SD.SessionCart,1);
        }
        else
        {
            //update count
            _unitOfWorks.ShoppingCart.IncrementCount(cartFromDb, obj.Count);
        }
        _unitOfWorks.Save();

        return RedirectToAction(nameof(Index));
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
