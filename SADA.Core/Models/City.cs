using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADA.Core.Models;

public class City
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public float? ShippingFees { get; set; }
    public int GovernorateId { get; set; }

    [ForeignKey("GovernorateId")]
    [ValidateNever]
    public Governorate Governorate { get; set; }
}
