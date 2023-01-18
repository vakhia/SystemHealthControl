using System.ComponentModel.DataAnnotations;

namespace CA.Application.Medicines.Queries.GetMedicines;

public class MedicineDiseaseUnit
{
    [Required] 
    public Guid Id { get; set; }
    
    [Required] 
    public string Title { get; set; }

    [Required] 
    public string Description { get; set; }
}