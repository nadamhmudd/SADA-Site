using Microsoft.EntityFrameworkCore;

namespace SADA.Core.Entities;

[Owned]
public class ProductColor
{
    public int Id { get; set; }
    public string  HashValue { get; set; }
}