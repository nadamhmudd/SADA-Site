using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADA.Core.Models;
public class ApplicationUser : IdentityUser
{
    [Required] 
    public string Name { get; set; }
    public string? StreetAddress { get; set; }
    public int CityId { get; set; }
    [ForeignKey("CityId")]
    [ValidateNever] 
    public City City { get; set; }

}