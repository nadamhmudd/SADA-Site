using Microsoft.AspNetCore.Mvc.Rendering;

namespace SADA.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class ProductController : Controller
{
    #region Props
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnviroment; //to access www root
    private readonly IFileHandler _fileHandler;
    #endregion

    #region Constructor(s)
    public ProductController(IUnitOfWork unitOfWork, 
        IWebHostEnvironment hostEnvironment, 
        IFileHandler fileHandler)
    {
        _unitOfWork = unitOfWork;
        _hostEnviroment = hostEnvironment;
        _fileHandler = fileHandler;
    }
    #endregion

    #region Actions
    public IActionResult Index() => View();

    //Build one method can deals with create and update 
    public IActionResult ProductForm(int? id)
    {
        //Initialize Model
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text  = i.Name,
                Value = i.Id.ToString(),
            }),
        };

        if (id is null || id == 0)
        {
            //Create 
            return View(productVM);
        }
        else
        {
            //Update
            //Retrieve product from Db
            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
           
            if (productVM.Product is null)
                return NotFound();

            return View(productVM);
        }
    }
 
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ProductForm(ProductVM obj, IFormFile? file)
    {
        if (ModelState.IsValid) //depends on constraints in the model
        {
            string key;

            if (file != null)
            {
                string posterUrl =  _fileHandler.Image.Upload(file,Path.Combine(_hostEnviroment.WebRootPath, SD.ProductImagespath));

                if (!posterUrl.Contains('\\'))
                { //not path
                    TempData["error"] = posterUrl; //error message
                    return View(obj);
                }

                //delete old image if updated
                if (obj.Product.CoverUrl != null)
                {
                    _fileHandler.Image.Delete(obj.Product.CoverUrl);
                }

                obj.Product.CoverUrl = posterUrl;
            }

            if (obj.Product.Id == 0)//store method 
            {
                _unitOfWork.Product.Add(obj.Product);
                key = "Created";
            }
            
            else //update method 
            {
                _unitOfWork.Product.Update(obj.Product);
                key = "Updated";
            }
            
            _unitOfWork.Save();
            
            TempData["success"] = $"Product {key} Successfully";

            return RedirectToAction("Index");
        }
        return View(obj);
    }
    #endregion
}