using Appointment.Aggregator.Models;

namespace Appointment.Aggregator.Interfaces;

public interface IUserService
{
    Task<UserModel> GetUser(string userId);
}