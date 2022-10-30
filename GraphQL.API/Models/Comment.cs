using System.ComponentModel.DataAnnotations;

namespace GraphQL.API.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int AuthorId { get; set; }
    
    [Required]
    public string Message { get; set; }

    [Required]
    public bool IsVisible { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}