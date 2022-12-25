using EntityFramework.API.Errors;
using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.API.Controllers;

public class AppointmentController : BaseApiController
{
    private readonly IAppointmentService _appointmentService;
    private readonly ILogger<AppointmentController> _logger;

    public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
    {
        _appointmentService = appointmentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<AppointmentResponse>>> GetAppointments(
        [FromQuery] PaginationSpecificationParams specificationParams)
    {
        return Ok(await _appointmentService.GetAppointmentsAsync(specificationParams));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResponse>> GetAppointmentById(int id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);

        if (appointment == null)
        {
            return BadRequest(new ApiResponse(404));
        }

        return appointment;
    }

    [HttpPost]
    public async Task<ActionResult<CreateAppointmentRequest>> CreateAppointment(
        CreateAppointmentRequest appointmentRequest)
    {
        return await _appointmentService.CreateAppointmentAsync(appointmentRequest);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateAppointmentRequest>> UpdateAppointment(
        UpdateAppointmentRequest appointmentRequest)
    {
        return await _appointmentService.UpdateAppointmentAsync(appointmentRequest);
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteAppointmentRequest>> DeleteAppointment(
        DeleteAppointmentRequest appointmentRequest)
    {
        return await _appointmentService.DeleteAppointmentAsync(appointmentRequest);
    }
}