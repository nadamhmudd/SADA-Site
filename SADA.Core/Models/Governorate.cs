using System.ComponentModel.DataAnnotations;

namespace SADA.Core.Models;
public class Governorate
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public float ShippingFees { get; set; } //can't be null, not nullable type
}
