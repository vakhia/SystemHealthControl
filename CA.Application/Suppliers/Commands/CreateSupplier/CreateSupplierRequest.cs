using System.ComponentModel.DataAnnotations;

namespace CA.Application.Suppliers.Commands.CreateSupplier;

public class CreateSupplierRequest
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string PhotoPath { get; set; }
}