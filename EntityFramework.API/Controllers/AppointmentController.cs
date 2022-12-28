using EntityFramework.API.Errors;
using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<Pagination<AppointmentResponse>>> GetAppointments(
        [FromQuery] PaginationSpecificationParams specificationParams)
    {
        try
        {
            return Ok(await _appointmentService.GetAppointmentsAsync(specificationParams));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResponse>> GetAppointmentById(int id)
    {
        try
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);

            if (appointment == null)
            {
                return BadRequest(new ApiResponse(404));
            }

            return Ok(appointment);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateAppointmentRequest>> CreateAppointment(
        CreateAppointmentRequest appointmentRequest)
    {
        try
        {
            return Ok(await _appointmentService.CreateAppointmentAsync(appointmentRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<UpdateAppointmentRequest>> UpdateAppointment(
        UpdateAppointmentRequest appointmentRequest)
    {
        try
        {
            return Ok(await _appointmentService.UpdateAppointmentAsync(appointmentRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<DeleteAppointmentRequest>> DeleteAppointment(
        DeleteAppointmentRequest appointmentRequest)
    {
        try
        {
            return Ok(await _appointmentService.DeleteAppointmentAsync(appointmentRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }
}