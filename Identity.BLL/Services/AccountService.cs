using System.Text;
using Identity.BLL.Dtos.Responses;
using Identity.BLL.Interfaces;
using Identity.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.BLL.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMailService _mailService;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService,
        IMailService mailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mailService = mailService;
    }
    
    public async Task<string> ConfirmEmail(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return "User doesn't exist";
        }
        
        code = Encoding.UTF8.GetString(Convert.FromBase64String(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        var status = result.Succeeded
            ? "Success! Thank you for confirming email address"
            : "Failure! Something try again later!";

        return status;
    }

    public async Task<UserResponse> GetCurrentUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return new UserResponse()
        {
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
        };
    }

    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        var result =  await _userManager.FindByEmailAsync(email);

        return result == null;
    }
}