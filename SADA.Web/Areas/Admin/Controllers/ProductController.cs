using Microsoft.AspNetCore.Mvc;
using SADA.Core.Models;
using SADA.Core.Interfaces;

namespace SADA.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    // To achieve dependency injection
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


    public IActionResult Index() => View(_unitOfWork.Product.GetAll());

    //GET
    public IActionResult Create() => View();
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product obj)
    {
        if (ModelState.IsValid) //depends on constraints in the model
        {
            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";

            return RedirectToAction("Index");
        }
        TempData["error"] = "Faild!!!";
        return View(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var productFromDb = _unitOfWork.Product.GetById((int)id);
        if (productFromDb is null) 
            return NotFound();

        return View(productFromDb);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid) //depends on constraints in the model
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfully";

            return RedirectToAction("Index");
        }

        return View(obj);
    }

    //GET
    public IActionResult Delete(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var productFromDb = _unitOfWork.Product.GetById((int)id);
        if (productFromDb is null)
            return NotFound();

        return View(productFromDb);
    }
    //POST
    [HttpPost/*,ActionName("Delete")*/]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Product obj)
    {
        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Product deleted successfully";

        return RedirectToAction("Index");
    }
}

