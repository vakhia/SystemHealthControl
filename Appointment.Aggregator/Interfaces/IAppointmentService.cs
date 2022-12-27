using Appointment.Aggregator.Models;

namespace Appointment.Aggregator.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentModel> GetAppointmentById(int appointmentId);
}