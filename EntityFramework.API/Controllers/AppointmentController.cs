using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IGenericRepository<Appointment> _appointmentsRepository;
    private readonly IGenericRepository<MedicalExamination> _examinationsRepository;

    public AppointmentController(IGenericRepository<Appointment> appointmentsRepository,
        IGenericRepository<MedicalExamination> examinationsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
        _examinationsRepository = examinationsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Appointment>>> GetAppointments()
    {
        var specification = new AppointmentsWithExaminationsSpecifications();
        var appointments = await _appointmentsRepository.ListAsync(specification);

        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointmentById(int id)
    {
        var specification = new AppointmentsWithExaminationsSpecifications(id);
        var appointment = await _appointmentsRepository.GetEntityWithSpec(specification);

        return Ok(appointment);
    }
}