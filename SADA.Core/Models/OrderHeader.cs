using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADA.Core.Models;
public class OrderHeader
{
    public int Id { get; set; }

    public string ApplicationUserId { get; set; }
    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }
    
    public double OrderTotal { get; set; } //not null
    public DateTime OrderDate{ get; set; } //not null
    public string OrderStatus { get; set; }//null

    public string PaymentMethodId { get; set; }
    [ForeignKey("PaymentMethodId")]
    [ValidateNever]
    public PaymentMethod PaymentMethod { get; set; }
    public string PaymentStatus { get; set; } //null
    public DateTime PaymentDate { get; set; } //not null
    public string SessionId { get; set; } //null
    public string PaymentIntentId { get; set; } //null

    public DateTime? ShippingDate { get; set; }//null
    public string Carrier { get; set; }//null

    [Required]
    public string Name { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string StreetAddress { get; set; }
    public int CityId { get; set; }
    [ForeignKey("CityId")]
    [ValidateNever]
    public City City { get; set; }
}
