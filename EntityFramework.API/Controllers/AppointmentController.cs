using AutoMapper;
using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.API.Controllers;

public class AppointmentController : BaseApiController
{
    private readonly IGenericRepository<Appointment> _appointmentsRepository;
    private readonly IGenericRepository<MedicalExamination> _examinationsRepository;
    private readonly IMapper _mapper;

    public AppointmentController(IGenericRepository<Appointment> appointmentsRepository,
        IGenericRepository<MedicalExamination> examinationsRepository, IMapper mapper)
    {
        _appointmentsRepository = appointmentsRepository;
        _examinationsRepository = examinationsRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<AppointmentResponse>>> GetAppointments(
        [FromQuery] AppointmentSpecParams appointmentParams)
    {
        var specification = new AppointmentsWithExaminationsSpecifications(appointmentParams);
        var appointments = await _appointmentsRepository.ListAsync(specification);
        var countSpecification = new AppointmentWithFiltersForCountSpecification(appointmentParams);
        var totalItems = await _appointmentsRepository.CountAsync(countSpecification);

        var data = _mapper
            .Map<IReadOnlyList<Appointment>, IReadOnlyList<AppointmentResponse>>(appointments);

        return Ok(new Pagination<AppointmentResponse>(appointmentParams.PageIndex, appointmentParams.PageSize,
            totalItems, data));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResponse>> GetAppointmentById(int id)
    {
        var specification = new AppointmentsWithExaminationsSpecifications(id);
        var appointment = await _appointmentsRepository.GetEntityWithSpec(specification);

        return _mapper.Map<Appointment, AppointmentResponse>(appointment);
    }
}