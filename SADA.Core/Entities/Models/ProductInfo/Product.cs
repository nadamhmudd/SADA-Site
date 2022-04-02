using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SADA.Core.Entities;
public class Product : BaseEntity
{
    [MaxLength(20)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Display(Name = "Original Price"), Range(0, double.MaxValue, ErrorMessage = "Price Can't be Negative!!")]
    public double Price { get; set; } //not null

    [ValidateNever, Display(Name = "Cover Image")]
    public string CoverUrl { get; set; }

    [Display(Name = "Stock Count")]
    public int StockCount { get; set; } //not null

    public bool OnSale { get; set; } = false;

    [Display(Name = "Discount Amount")]
    public double DiscountAmount { get; set; } = 0.0;

    [Display(Name = "Discount %")]
    public double DiscountPercentage { get; set; } = 0.0;

    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public List<ProductColor> Colors { get; set; }
    public List<ProductSize> Sizes { get; set; }
    public List<ProductImage> Imaages { get; set; }
}

