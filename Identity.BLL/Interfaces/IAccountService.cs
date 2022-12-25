using Identity.BLL.Dtos.Requests;
using Identity.BLL.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Identity.BLL.Interfaces;

public interface IAccountService
{
    Task<string> ConfirmEmail(string userId, string code);
    Task<UserResponse> GetCurrentUser(string email);
    Task<bool> CheckEmailExistsAsync(string email);
    Task<bool> SyncUserById(string userId);

}