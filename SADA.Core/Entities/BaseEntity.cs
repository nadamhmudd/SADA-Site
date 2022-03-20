global using System.ComponentModel.DataAnnotations;

namespace SADA.Core.Entities;
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}