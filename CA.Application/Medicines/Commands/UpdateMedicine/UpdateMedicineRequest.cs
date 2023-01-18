using System.ComponentModel.DataAnnotations;

namespace CA.Application.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineRequest
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; }

    [Required]
    public Guid SupplierId { get; set; }

    [Required]
    public string Country { get; set; }

    [Required]
    public string Contraindication { get; set; } = "none";

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    [Range(double.Epsilon, double.MaxValue)]
    public float Price { get; set; }

    [Required]
    public string PhotoPath { get; set; }

    [Required]
    public List<Guid> DiseasesIds { get; set; }
}