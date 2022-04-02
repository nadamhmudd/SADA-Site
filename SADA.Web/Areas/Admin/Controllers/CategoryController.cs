namespace SADA.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    #region Props
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructor(s)
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Actions
    public IActionResult Index() => View();

    public IActionResult Create() => View();

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var categoryFromDb = _unitOfWork.Category.GetById((int)id);

        if (categoryFromDb is null)
            return NotFound();

        return View(categoryFromDb);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (_upsert(obj))
            return RedirectToAction("Index");

        return View(obj);
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (_upsert(obj))
            return RedirectToAction("Index");

        return View(obj);
    }
    #endregion

    #region Helper Methods
    private bool _upsert(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");

        if (ModelState.IsValid) //depends on constraints in the model
        {
            string key;

            if (obj.Id == 0)
            {
                _unitOfWork.Category.Add(obj);
                key = "Created";

            }
            else
            {
                _unitOfWork.Category.Update(obj);
                key = "updated";
            }
            _unitOfWork.Save();

            TempData["success"] = $"Category {key} successfully";

            return true;
        }
        return false;
    }
    #endregion
}