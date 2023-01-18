using System.ComponentModel.DataAnnotations;
using CA.Domain.Base;

namespace CA.Domain.Suppliers;

public class Supplier : BaseModel
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    public string PhotoPath { get; set; }
}