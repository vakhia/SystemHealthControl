using AutoMapper;
using EntityFramework.BLL.Consumers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;
using Shared.Models.Queues;

namespace EntityFramework.BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserRequestQueue> CreateUserAsync(UserRequestQueue userRequest)
    {
        var user = _mapper.Map<UserRequestQueue, User>(userRequest);
        _unitOfWork.Repository<User>().Add(user);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return userRequest;
    }

    public async Task<UserRequestConsumer> GetUserByIdAsync(string id)
    {
        var specification = new UsersSpecification(id);
        return _mapper.Map<User, UserRequestConsumer>(
            await _unitOfWork.Repository<User>().GetEntityWithSpec(specification));
    }
}