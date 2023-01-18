using System.ComponentModel.DataAnnotations;

namespace CA.Application.Diseases.Commands.CreateDisease;

public class CreateDiseaseRequest
{
    [Required] 
    public string Title { get; set; }

    [Required] 
    public string Description { get; set; }

    [Required]
    public string PhotoPath { get; set; }
}