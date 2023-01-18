using System.ComponentModel.DataAnnotations;

namespace CA.Application.Suppliers.Queries.GetSuppliers;

public class SuppliersResponse
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