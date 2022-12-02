using System.Security.Claims;
using Identity.BLL.Dtos.Requests;
using Identity.BLL.Dtos.Responses;
using Identity.BLL.Interfaces;
using Identity.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
        ITokenService tokenService)
    { 
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
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
            Token = _tokenService.CreateToken(user),
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(UserRegisterRequest userRegisterRequest)
    {
        var user = new User()
        {
            UserName = userRegisterRequest.FirstName + userRegisterRequest.Email,
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
            Token = _tokenService.CreateToken(user),
        };
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.FindByEmailAsync(email);

        return new UserResponse()
        {
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
        };
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }
}