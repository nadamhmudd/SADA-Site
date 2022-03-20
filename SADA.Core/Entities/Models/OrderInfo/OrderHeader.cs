using SADA.Core.Entities;

namespace SADA.Core.Models;

public class OrderHeader : BaseEntity
{
    public List<OrderDetail> Items { get; set; }

    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public double OrderTotal { get; set; } 
    public string? OrderStatus { get; set; }
    public string? PaymentStatus { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }
    public DateTime ShippingDate { get; set; }
    public string? Carrier { get; set; }

    //client data
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string StreetAddress { get; set; }
   
    public int CityId { get; set; }
    public City City { get; set; }
}
