using Microsoft.EntityFrameworkCore;
using SADA.Core.Entities;

namespace SADA.Core.Models;
public class City : BaseEntity
{
    public string Name { get; set; }
    
    public float? ShippingFees { get; set; }

    public int GovernorateId { get; set; }
    public Governorate Governorate { get; set; }
}
