using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADA.Core.Entities;

public class ShoppingCart : BaseEntity
{
    public string UserId { get; set; }

    [ValidateNever, ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    public int ProductId { get; set; }

    [ValidateNever, ForeignKey("ProductId")]
    public Product Product { get; set; }

    public int Count { get; set; }  

    public string Color { get; set; }
    public string Size { get; set; }
}
