using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SADA.Core.Models;

namespace SADA.Core.ViewModels;
public class ProductVM
{
    public Product Product { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; } 
}
