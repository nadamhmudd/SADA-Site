using Microsoft.EntityFrameworkCore;
using SADA.Core.Entities;

namespace SADA.Core.Models;

[Owned]
public class ShoppingCart : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Count { get; set; }  
}
