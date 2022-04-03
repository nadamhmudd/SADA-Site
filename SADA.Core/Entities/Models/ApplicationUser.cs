using Microsoft.AspNetCore.Identity;

namespace SADA.Core.Entities;
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    
    public string? StreetAddress { get; set; }
    
    public int? CityId { get; set; }
    public City? City { get; set; }
}