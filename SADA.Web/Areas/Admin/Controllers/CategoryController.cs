using Microsoft.AspNetCore.Mvc;
using SADA.Core.Models;
using SADA.Core.Interfaces;
using SADA.Service;
using Microsoft.AspNetCore.Authorization;

namespace SADA.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    // To achieve dependency injection
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public IActionResult Index() => View();

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

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var list = _unitOfWork.Category.GetAll(null, o => o.DisplayOrder);
        return Json(new { data = list });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(p => p.Id == id);
        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting!" });
        }

        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
    }
    #endregion
    ////GET
    //public IActionResult Delete(int? id)
    //{
    //    if (id is null || id == 0)
    //        return NotFound();

    //    var categoryFromDb = _unitOfWork.Category.GetById((int)id);
    //    if (categoryFromDb is null)
    //        return NotFound();

    //    return View(categoryFromDb);
    //}
    ////POST
    //[HttpPost/*,ActionName("Delete")*/]
    //[ValidateAntiForgeryToken]
    //public IActionResult Delete(Category obj)
    //{
    //    _unitOfWork.Category.Remove(obj);
    //    _unitOfWork.Save();
    //    TempData["success"] = "Category deleted successfully";

    //    return RedirectToAction("Index");
    //}
}

