using System.ComponentModel.DataAnnotations;

namespace CA.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierRequest
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string PhotoPath { get; set; }
}