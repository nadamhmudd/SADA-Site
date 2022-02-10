using Microsoft.AspNetCore.Mvc;
using SADA.Core.Interfaces;
using SADA.Core.Models;
using SADA.Service;
using System.Diagnostics;

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

    public IActionResult Show(int? id)
    {
        ShoppingCart obj = new()
        {
            Count = 1,
            Product = _unitOfWorks.Product.GetFirstOrDefault(o => o.Id==id, includeProperties: "Category")
        };

        return View(obj);
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
