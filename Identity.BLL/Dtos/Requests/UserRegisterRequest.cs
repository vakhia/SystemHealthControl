namespace Identity.BLL.Dtos.Requests;

public class UserRegisterRequest
{
    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}