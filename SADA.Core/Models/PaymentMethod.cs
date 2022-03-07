using System.ComponentModel.DataAnnotations;

namespace SADA.Core.Models;

public class PaymentMethod
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
