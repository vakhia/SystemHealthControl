using System.Text;
using Identity.BLL.Dtos.Requests;
using Identity.BLL.Dtos.Responses;
using Identity.BLL.Interfaces;
using Identity.DAL.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Identity.BLL.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMailService _mailService;
    private readonly IBus _bus;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService,
        IMailService mailService, IBus bus)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mailService = mailService;
        _bus = bus;
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
        if (result.Succeeded)
        {
            await _bus.Publish(new UserRequestQueue()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
            });
        }

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
        var result = await _userManager.FindByEmailAsync(email);

        return result == null;
    }

    public async Task<bool> SyncUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _bus.Publish(new UserRequestQueue()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
        });
        return true;
    }
}