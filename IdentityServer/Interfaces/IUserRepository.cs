using IdentityServer.Dtos;

namespace IdentityServer.Interfaces;

public interface IUserRepository
{
    Task<UserResponse> Register(UserRegisterRequest request);

    Task<IEnumerable<UserResponse>> GetUsersAsync();

    Task<UserResponse> GetByIdAsync(string id);

    Task<bool> DeleteAsync(string id);
}