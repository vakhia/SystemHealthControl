using Identity.DAL.Models;

namespace Identity.BLL.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}