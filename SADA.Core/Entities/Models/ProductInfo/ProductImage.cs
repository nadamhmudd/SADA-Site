using Microsoft.EntityFrameworkCore;

namespace SADA.Core.Entities;

[Owned]
public class ProductImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
}
