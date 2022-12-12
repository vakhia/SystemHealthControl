using AutoMapper;
using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Services;

public class AppointmentService: IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }   

    public async Task<CreateAppointmentRequest> CreateAppointmentAsync(CreateAppointmentRequest appointmentRequest)
    {
        var appointment = _mapper.Map<CreateAppointmentRequest, Appointment>(appointmentRequest);
        _unitOfWork.Repository<Appointment>().Add(appointment);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return appointmentRequest;
    }

    public async Task<Pagination<AppointmentResponse>> GetAppointmentsAsync(PaginationSpecificationParams specificationParams)
    {
        var specification = new AppointmentsWithExaminationsSpecifications(specificationParams);
        var appointments = await _unitOfWork.Repository<Appointment>().ListAsync(specification);
        var countAppointWithExaminationsSpecification = new AppointmentWithFiltersForCountSpecification(specificationParams);
        var data = _mapper.Map<IReadOnlyList<Appointment>, IReadOnlyList<AppointmentResponse>>(appointments);
        var totalItems =  await _unitOfWork.Repository<Appointment>().CountAsync(countAppointWithExaminationsSpecification);
        
        return new Pagination<AppointmentResponse>(specificationParams.PageIndex, specificationParams.PageSize,
            totalItems, data);
    }

    public async Task<AppointmentResponse> GetAppointmentByIdAsync(int id)
    {
        var specification = new AppointmentsWithExaminationsSpecifications(id);
        return _mapper.Map<Appointment, AppointmentResponse>(
            await _unitOfWork.Repository<Appointment>().GetEntityWithSpec(specification));
    }

    public async Task<UpdateAppointmentRequest> UpdateAppointmentAsync(UpdateAppointmentRequest appointmentRequest)
    {
        var appointment = _mapper.Map<UpdateAppointmentRequest, Appointment>(appointmentRequest);
        _unitOfWork.Repository<Appointment>().Update(appointment);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return appointmentRequest;
    }

    public async Task<DeleteAppointmentRequest> DeleteAppointmentAsync(DeleteAppointmentRequest appointmentRequest)
    {
        var appointment = _mapper.Map<DeleteAppointmentRequest, Appointment>(appointmentRequest);
        _unitOfWork.Repository<Appointment>().Delete(appointment);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return appointmentRequest;
    }
}