using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Interfaces;

public interface IAppointmentService
{
    Task<CreateAppointmentRequest> CreateAppointmentAsync(CreateAppointmentRequest appointmentRequest);
    Task<Pagination<AppointmentResponse>> GetAppointmentsAsync(PaginationSpecificationParams specificationParams);
    Task<AppointmentResponse> GetAppointmentByIdAsync(int id);
    Task<UpdateAppointmentRequest> UpdateAppointmentAsync(UpdateAppointmentRequest appointmentRequest);
    Task<DeleteAppointmentRequest> DeleteAppointmentAsync(DeleteAppointmentRequest appointmentRequest);


}