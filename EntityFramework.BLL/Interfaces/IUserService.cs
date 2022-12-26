using EntityFramework.BLL.Consumers;
using EntityFramework.BLL.Dtos.Requests;
using Shared.Models.Queues;

namespace EntityFramework.BLL.Interfaces;

public interface IUserService
{
    Task<UserRequestQueue> CreateUserAsync(UserRequestQueue userRequest);

    Task<UserRequestConsumer> GetUserByIdAsync(string id);
}