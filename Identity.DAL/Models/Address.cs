using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.Models;

public class Address
{
    
    public int Id { get; set; }
    
    public string Street { get; set; }
    
    public string City { get; set; }
    
    public string Zip { get; set; }
    
    [Required]
    public string UserId { get; set; }
    public User Users { get; set; }
}