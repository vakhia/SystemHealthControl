using System.ComponentModel.DataAnnotations;
using CA.Application.Medicines.Queries.GetMedicines;
using CA.Domain.Suppliers;

namespace CA.Application.Medicines.Queries.GetMedicineById;

public class  MedicineResponse
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; }

    [Required]
    public Supplier Supplier { get; set; }

    [Required]
    public string Country { get; set; }

    public string Contraindication { get; set; } = "none";

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    [Range(double.Epsilon, double.MaxValue)]
    public float Price { get; set; }
    
    public string PhotoPath { get; set; }
    
    public ICollection<MedicineDiseaseUnit> Diseases { get; set; }
}