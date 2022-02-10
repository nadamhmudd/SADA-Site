using System.ComponentModel.DataAnnotations;

namespace SADA.Core.Models;

public class ShoppingCart
{
    public Product Product { get; set; }

    [Range(1,100)]
    public int Count { get; set; }  
}
