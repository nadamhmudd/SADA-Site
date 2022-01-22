using System.ComponentModel.DataAnnotations;

namespace SADA.Core.Models;

public class Category
{
    [Key] //Primary Key
    public int Id { get; set; }

    [Required] //can't be null
    public string Name { get; set; }

    [Display(Name = "Display Order")]
    [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100 only!!")]
    public int DisplayOrder { get; set; } //can't be null becuse it is int "not nullable type"
    
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}

