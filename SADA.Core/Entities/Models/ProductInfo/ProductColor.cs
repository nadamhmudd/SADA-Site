using Microsoft.EntityFrameworkCore;

namespace SADA.Core.Models;

[Owned]
public class ProductColor
{
    public int Id { get; set; }
    public string ColorName { get; set; }
}
