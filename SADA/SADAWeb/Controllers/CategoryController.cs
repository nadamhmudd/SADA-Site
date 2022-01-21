using Microsoft.AspNetCore.Mvc;
using SADA.Database;
using SADA.Models;

namespace SADA.Controllers;
public class CategoryController : Controller
{
    // To achieve dependency injection
    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db) => _db = db;
    public IActionResult Index()
    {
        IEnumerable<Category> categoryList = _db.Categories;
        return View(categoryList);
    }


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
            _db.Add(obj);
            _db.SaveChanges();
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

        var categoryFromDb = _db.Categories.Find(id);
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
            _db.Update(obj);
            _db.SaveChanges();
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

        var categoryFromDb = _db.Categories.Find(id);
        if (categoryFromDb is null)
            return NotFound();

        return View(categoryFromDb);
    }
    //POST
    [HttpPost/*,ActionName("Delete")*/]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category obj)
    {
        _db.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index");
    }
}

