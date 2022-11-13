using EntityFramework.DAL.Models;

namespace EntityFramework.DAL.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment> GetAppointmentByIdAsync(int id);
    Task<IReadOnlyList<Appointment>> GetAppointmentsAsync();
}