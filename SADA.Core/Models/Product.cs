using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADA.Core.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price Can't be Negative!!")]
    public double Price { get; set; }

    [Display(Name = "Discount Percentage")]
    public double DiscountAmount { get; set; } = 0.0;

    [Display(Name ="Discount Amount")]
    public double DiscountPercentage { get; set; } = 0.0;

    //[ValidateNever]
    [Required]
    public string CoverUrl { get; set; }

    //Foreigen Key   
    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    //[ValidateNever]
    public Category Category { get; set; }
    [ForeignKey("CategoryId")] //not requiered because outo mapping done 

    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}

