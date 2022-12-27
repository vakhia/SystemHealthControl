using Appointment.Aggregator.Interfaces;
using Appointment.Aggregator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Aggregator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAppointmentService _appointmentService;
    
    public AppointmentController(IUserService userService, IAppointmentService appointmentService)
    {
        _userService = userService;
        _appointmentService = appointmentService;
    }
    
    [HttpGet("{appointmentId}")] 
    public async Task<ActionResult<ExtendedAppointmentModel>> GetAppointment(int appointmentId)
    {
        var appointment = await _appointmentService.GetAppointmentById(appointmentId);
        var doctor = await _userService.GetUser(appointment.DoctorId);
        var client = await _userService.GetUser(appointment.ClietId);

        var result = new ExtendedAppointmentModel()
        {
            Id = appointmentId,
            Doctor = doctor,
            Client = client,
            Title= appointment.Title,
            Description = appointment.Description,
            ExtraInformation = appointment.ExtraInformation,
            StartDate = appointment.StartDate,
            EndDate = appointment.EndDate,
        };

        return Ok(result);
    }   
}