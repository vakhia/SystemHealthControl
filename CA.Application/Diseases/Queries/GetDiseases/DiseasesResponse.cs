using System.ComponentModel.DataAnnotations;

namespace CA.Application.Diseases.Queries.GetDiseases;

public class DiseasesResponse
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