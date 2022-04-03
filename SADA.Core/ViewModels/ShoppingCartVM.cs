using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SADA.Core.ViewModels;
public class ShoppingCartVM
{
    public OrderHeader OrderHeader { get; set; }

    public IEnumerable<ShoppingCart> ListCart  { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> PaymentMethod { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> Governorates { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> Cities { get; set; }
}
