using Microsoft.AspNetCore.Mvc;
using SADA.Core.Models;
using SADA.Core.Interfaces;

namespace SADA.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    // To achieve dependency injection
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


    public IActionResult Index() => View(_unitOfWork.Category.GetAll(o=>o.DisplayOrder));

    //GET
    public IActionResult Create() => View();
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");

        if (ModelState.IsValid) //depends on constraints in the model
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";

            return RedirectToAction("Index");
        }
        
        return View(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var categoryFromDb = _unitOfWork.Category.GetById((int)id);
        if (categoryFromDb is null) 
            return NotFound();

        return View(categoryFromDb);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");

        if (ModelState.IsValid) //depends on constraints in the model
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";

            return RedirectToAction("Index");
        }

        return View(obj);
    }

    //GET
    public IActionResult Delete(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var categoryFromDb = _unitOfWork.Category.GetById((int)id);
        if (categoryFromDb is null)
            return NotFound();

        return View(categoryFromDb);
    }
    //POST
    [HttpPost/*,ActionName("Delete")*/]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category obj)
    {
        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index");
    }
}

