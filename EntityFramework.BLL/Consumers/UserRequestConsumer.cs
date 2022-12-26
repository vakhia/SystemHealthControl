using EntityFramework.BLL.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Models.Queues;

namespace EntityFramework.BLL.Consumers;

public class UserRequestConsumer : IConsumer<UserRequestQueue>
{
    private readonly IUserService _userService;
    private readonly ILogger<UserRequestConsumer> _logger;
    public UserRequestConsumer(IUserService userService, ILogger<UserRequestConsumer> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<UserRequestQueue> context)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(context.Message.IdentityId);
            if (user != null)
            {
                return;
            }
            await _userService.CreateUserAsync(new UserRequestQueue()
            {
                IdentityId = context.Message.IdentityId,
                FirstName = context.Message.FirstName,
                SecondName = context.Message.SecondName,
            });
            _logger.LogInformation("User was created!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger.LogError(e.Message);
        }
    }
}