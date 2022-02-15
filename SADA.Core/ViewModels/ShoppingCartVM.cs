using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SADA.Core.Models;

namespace SADA.Core.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<ShoppingCart> ListCart  { get; set; }
    public double CartTotal { get; set; }
}
