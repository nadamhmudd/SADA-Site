using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SADA.Service;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADA.Core.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        
        [Required]
        public double OrderTotal { get; set; }
        [Required]
        public DateTime OrderDate{ get; set; }
        public string? OrderStatus { get; set; }

        [Required]
        public virtual int PaymentOptionsId { get; set; } = 0;
        [EnumDataType(typeof(SD.PaymentOptions))]
        public SD.PaymentOptions PaymentOption 
        {
            get => (SD.PaymentOptions)PaymentOptionsId;
            set => PaymentOptionsId = (int)value;
        }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }


        public DateTime? ShippingDate { get; set; }
        public string? Carrier { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }


        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Name { get; set; }
    }


}
