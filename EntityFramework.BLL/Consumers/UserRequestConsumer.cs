using EntityFramework.BLL.Dtos.Requests;
using MassTransit;

namespace EntityFramework.BLL.Consumers;

public class UserRequestConsumer : IConsumer<UserRequestQueue>
{
    public Task Consume(ConsumeContext<UserRequestQueue> context)
    {
        throw new NotImplementedException();
    }
}