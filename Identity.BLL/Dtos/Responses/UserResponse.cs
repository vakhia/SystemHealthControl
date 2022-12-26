namespace Identity.BLL.Dtos.Responses;

public class UserResponse
{
    public string Id { get; set; }
    
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public string Token { get; set; }
    
    public string? RefreshToken { get; set; }
}