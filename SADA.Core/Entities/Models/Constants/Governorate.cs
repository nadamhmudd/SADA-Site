using SADA.Core.Entities;

namespace SADA.Core.Models;
public class Governorate : BaseEntity
{
    public string Name { get; set; }
    public float ShippingFees { get; set; } //can't be null, not nullable type
}
