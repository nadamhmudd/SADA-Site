using Microsoft.EntityFrameworkCore;

namespace SADA.Core.Entities;

[Owned]
public class ShoppingCart : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Count { get; set; }  
}
