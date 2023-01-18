using System.ComponentModel.DataAnnotations;
using CA.Domain.Base;
using CA.Domain.Shared;

namespace CA.Domain.Diseases;

public class Disease : BaseModel
{
    [Required] 
    public string Title { get; set; }

    [Required] 
    public string Description { get; set; }

    public string PhotoPath { get; set; }

    public ICollection<MedicineDisease> Medicines { get; set; }
}