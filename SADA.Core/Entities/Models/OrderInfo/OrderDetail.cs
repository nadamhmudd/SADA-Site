using Microsoft.EntityFrameworkCore;

namespace SADA.Core.Models;

[Owned]
public class OrderDetail
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Count { get; set; }

    public double Price { get; set; }
}
