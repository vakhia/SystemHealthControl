using Identity.BLL.Dtos.Requests;
using Identity.DAL.Models;

namespace Identity.BLL.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);

    Task<RefreshToken> CreateRefreshToken(string userId);

    Task<string> VerifyAndGenerateToken(RefreshTokenRequest tokenRequest);
}