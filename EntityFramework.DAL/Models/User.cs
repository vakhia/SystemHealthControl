namespace EntityFramework.DAL.Models;

public class User : BaseModel
{
    public string IdentityId { get; set; }

    public string FirstName { get; set; }

    public string SecondName { get; set; }
}