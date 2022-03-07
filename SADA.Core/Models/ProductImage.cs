using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADA.Core.Models;
public class ProductImage
{
    public int Id { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    public byte IsCover { get; set; } = 0; //0 no, 1 yes "cover"
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [ValidateNever]
    public Product Product { get; set; }
}
