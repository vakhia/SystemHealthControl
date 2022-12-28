using System.Security.Claims;
using IdentityServer.Data;
using IdentityServer.Dtos;
using IdentityServer.Interfaces;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    private readonly IdentityServerDatabaseContext _databaseContext;

    public UserRepository(UserManager<User> userManager,
        IdentityServerDatabaseContext databaseContext)
    {
        _userManager = userManager;
        _databaseContext = databaseContext;
    }

    public async Task<UserResponse> Register(UserRegisterRequest userRegisterRequest)
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

        var result = _userManager.CreateAsync(user, userRegisterRequest.Password).Result;

        result = _userManager.AddClaimsAsync(user,
            new Claim[]
            {
                new Claim("role", "user")
            }).Result;

        await _databaseContext.SaveChangesAsync();

        return new UserResponse()
        {
            FirstName = userRegisterRequest.FirstName,
            SecondName = userRegisterRequest.SecondName,
            Email = userRegisterRequest.Email,
        };
    }

    public Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponse> GetByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return new UserResponse()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
        };
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        await _userManager.DeleteAsync(user);
        var result = await _databaseContext.SaveChangesAsync();

        if (result < 0)
        {
            return false;
        }

        return true;
    }
}