using SADA.Core.Entities;

namespace SADA.Core.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; }

    [Display(Name = "Display Order")]
    [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100 only!!")]
    public int DisplayOrder { get; set; } //can't be null becuse it is int "not nullable type"
}

