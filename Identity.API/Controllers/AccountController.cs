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
    private readonly IMailService _mailService;
    private readonly IAccountService _accountService;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
        ITokenService tokenService, IMailService mailService, IAccountService accountService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mailService = mailService;
        _accountService = accountService;
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
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, userRegisterRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(400);
        }

        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var emailBody = $"Please confirm your email address <a href=\"#URL#\">Click me</a>";
        var callbackUrl = Request.Scheme + "://" + Request.Host +
                          Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = emailToken });
        var body = emailBody.Replace("#URL#", System.Text.Encodings.Web.HtmlEncoder.Default.Encode(callbackUrl));
        var responseFromSendingMail = _mailService.SendEmail(body, user.Email);

        if (!responseFromSendingMail)
        {
            return BadRequest(400);
        }

        return Ok("Please, verify your email to log in!");
    }

    [HttpGet("ConfirmEmail")]
    public async Task<ActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return BadRequest(400);
        }
        
        return Ok(await _accountService.ConfirmEmail(userId, code));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        return await _accountService.GetCurrentUser(email);
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await _accountService.CheckEmailExistsAsync(email);
    }
}