using Microsoft.AspNetCore.Mvc;
using SADA.Core.Interfaces;
using SADA.Core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using SADA.Service;

namespace SADA.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    // To achieve dependency injection
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnviroment; //to access www root

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnviroment = hostEnvironment;
    }

    public IActionResult Index() => View();

    //Build one method can deals with create and update 
    //GET
    public IActionResult Upsert(int? id)
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
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {
        if (ModelState.IsValid) //depends on constraints in the model
        {
            //upload files in www root
            string wwwRootPath = _hostEnviroment.WebRootPath; //access web root path
            if(file != null)
            {
                //delete old image if not equal null
                if(obj.Product.CoverUrl != null) //update image
                {
                    DeleteImage(obj.Product.CoverUrl);
                }
                //preparing saved file in our projects 
                //generate unique file name
                var fileName  = Guid.NewGuid().ToString();
                //generate location to upload file here
                var uploads   = Path.Combine(wwwRootPath, @"images\products");
                //keep extension of file
                var extension = Path.GetExtension(file.FileName);
                //copy inputfile and save it
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                //save in Db
                obj.Product.CoverUrl = @$"\images\products\{fileName + extension}";
            }
        
            if (obj.Product.Id == 0)//store method 
            {
                _unitOfWork.Product.Add(obj.Product);
                TempData["success"] = "Product Created Successfully";
            }
            else //update method 
            {
                _unitOfWork.Product.Update(obj.Product);
                TempData["success"] = "Product Updated Successfully";
            }
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        return View(obj);
    }

    private void DeleteImage(string image)
    {
        var oldImagePath = Path.Combine(_hostEnviroment.WebRootPath, image.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath); //old image deleted
        }
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var productsList = _unitOfWork.Product.GetAll("Category", o => o.Id, SD.Descending);
        return Json(new { data = productsList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
        if(obj == null)
        {
            return Json(new { success = false, message = "Error while deleting!" });
        }

        if (obj.CoverUrl != null) //delete image
        {
            DeleteImage(obj.CoverUrl);
        }

        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
    }
    #endregion
}