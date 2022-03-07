using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADA.Core.Models;

public class ShoppingCart
{
    public int Id { get; set; }

    public string ApplicationUserID { get ; set; }   
    [ForeignKey("ApplicationUserID")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }

    public int ProductID { get; set; }
    [ForeignKey("ProductID")]
    [ValidateNever]
    public Product Product { get; set; }

    [Range(1,100)]
    public int Count { get; set; }  
}
