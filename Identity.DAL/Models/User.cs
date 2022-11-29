using Microsoft.AspNetCore.Identity;

namespace Identity.DAL.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; }
}