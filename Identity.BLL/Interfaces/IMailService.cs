namespace Identity.BLL.Interfaces;

public interface IMailService
{
    public bool SendEmail(string email, string body);
}