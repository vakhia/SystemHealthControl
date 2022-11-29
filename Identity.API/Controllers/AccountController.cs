using Identity.BLL.Dtos.Requests;
using Identity.BLL.Dtos.Responses;
using Identity.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(UserLoginRequest userLoginRequest)
    {
        var user = await _userManager.FindByEmailAsync(userLoginRequest.Email);

        if (user == null)
        {
            return Unauthorized(401);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginRequest.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized(401);
        }

        return new UserResponse()
        {
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            Email = user.Email,
            Token = "Token",
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(UserRegisterRequest userRegisterRequest)
    {
        var user = new User()
        {
            FirstName = userRegisterRequest.FirstName,
            SecondName = userRegisterRequest.SecondName,
            Email = userRegisterRequest.Email,
            DateOfBirth = userRegisterRequest.DateOfBirth,
        };

        var result = await _userManager.CreateAsync(user, userRegisterRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(400);
        }

        return new UserResponse()
        {
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            Email = user.Email,
            Token = "Token",
        };
    }
}