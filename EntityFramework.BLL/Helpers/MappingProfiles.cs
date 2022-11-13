using AutoMapper;
using EntityFramework.BLL.Dtos;
using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Helpers;

public class MappingProfiles : Profile
{
    protected MappingProfiles()
    {
        CreateMap<Appointment, AppointmentResponse>();
    }
}