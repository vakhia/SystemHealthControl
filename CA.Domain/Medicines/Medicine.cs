using System.ComponentModel.DataAnnotations;
using CA.Domain.Base;
using CA.Domain.Shared;
using CA.Domain.Suppliers;

namespace CA.Domain.Medicines;

public class Medicine : BaseModel
{
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

    public ICollection<MedicineDisease> Diseases { get; set; } = new List<MedicineDisease>();

}