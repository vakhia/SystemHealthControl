using System.ComponentModel.DataAnnotations;

namespace CA.Application.Diseases.Commands.UpdateDisease;

public class UpdateDiseaseRequest
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