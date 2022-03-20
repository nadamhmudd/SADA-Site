using Microsoft.EntityFrameworkCore;

namespace SADA.Core.Models;

[Owned]
public class ProductSize
{
    public int Id { get; set; }
    public string Size { get; set; }
}
