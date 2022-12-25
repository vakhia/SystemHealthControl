using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.BLL.Dtos.Requests;
using Identity.BLL.Interfaces;
using Identity.DAL.Data;
using Identity.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.BLL.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;
    private readonly IdentityDatabaseContext _databaseContext;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration configuration, IdentityDatabaseContext databaseContext, UserManager<User> userManager)
    {
        _configuration = configuration;
        _databaseContext = databaseContext;
        _userManager = userManager;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials,
            Issuer = _configuration["Token:Issuer"],
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<RefreshToken> CreateRefreshToken(string userId)
    {
        var refreshToken = new RefreshToken()
        {
            Token = GenerateRandomString(19),
            AddedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            IsUsed = false,
            UserId = userId,
        };

        await _databaseContext.RefreshTokens.AddAsync(refreshToken);
        await _databaseContext.SaveChangesAsync();

        return refreshToken;
    }

    public async Task<string> VerifyAndGenerateToken(RefreshTokenRequest tokenRequest)
    {
        //TODO NEED TO REFACTOR THIS 
        var tokenHandler = new JwtSecurityTokenHandler();
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();
        var tokenVerification = tokenHandler.ValidateToken(tokenRequest.Token, new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Token:Key"))),
            ValidateIssuer = false,
            ValidateAudience = false,
        }, out var validatedToken);
        if (validatedToken is JwtSecurityToken jwtSecurityToken) {
            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature);
            if (result == false)
            {
                return null;
            }
        }

        var utcExpiryDate =
            long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
        if (expiryDate < DateTime.Now)
        {
            return "Expired token";
        }

        var storedToken = await _databaseContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

        if (storedToken == null)
        {
            return "Invalid Tokens";
        }

        storedToken.IsUsed = true;
        _databaseContext.RefreshTokens.Update(storedToken);
        await _databaseContext.SaveChangesAsync();

        var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
        return CreateToken(dbUser);
    }

    private string GenerateRandomString(int length)
    {
        var random = new Random();
        var chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTimeVal;
    }
}