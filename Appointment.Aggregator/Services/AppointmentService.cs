using Appointment.Aggregator.Extensions;
using Appointment.Aggregator.Interfaces;
using Appointment.Aggregator.Models;

namespace Appointment.Aggregator.Services;

public class AppointmentService: IAppointmentService
{
    private readonly HttpClient _httpClient;
    
    public AppointmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<AppointmentModel> GetAppointmentById(int appointmentId)
    {
        var response = await _httpClient.GetAsync($"/api/Appointment/{appointmentId}");
        return await response.ReadContentAs<AppointmentModel>();
    }
}