using System.ComponentModel.DataAnnotations;

namespace CA.Application.Suppliers.Queries.GetSupplierById;

public class SupplierResponse
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string PhotoPath { get; set; }
    
}