using System.ComponentModel.DataAnnotations;

namespace CA.Domain.Base;

public class BaseModel
{
    [Required]
    public Guid Id { get; set; }
}