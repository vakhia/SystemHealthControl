using AutoMapper;
using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.DAL.Models;
using Shared.Models.Queues;

namespace EntityFramework.BLL.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Appointment, AppointmentResponse>();

        CreateMap<Appointment, CreateAppointmentRequest>();
        CreateMap<CreateAppointmentRequest, Appointment>();

        CreateMap<Appointment, UpdateAppointmentRequest>();
        CreateMap<UpdateAppointmentRequest, Appointment>();

        CreateMap<Appointment, DeleteAppointmentRequest>();
        CreateMap<DeleteAppointmentRequest, Appointment>();

        CreateMap<MedicalExamination, MedicalExaminationResponse>();
        CreateMap<MedicalExaminationResponse, MedicalExamination>();

        CreateMap<MedicalExamination, CreateMedicalExaminationRequest>();
        CreateMap<CreateMedicalExaminationRequest, MedicalExamination>();

        CreateMap<MedicalExamination, DeleteTreatmentRequest>();
        CreateMap<DeleteTreatmentRequest, MedicalExamination>();

        CreateMap<Treatment, TreatmentResponse>();
        CreateMap<TreatmentResponse, Treatment>();

        CreateMap<Treatment, CreateTreatmentRequest>();
        CreateMap<CreateTreatmentRequest, Treatment>();

        CreateMap<Treatment, UpdateTreatmentRequest>();
        CreateMap<UpdateTreatmentRequest, Treatment>();

        CreateMap<Treatment, DeleteTreatmentRequest>();
        CreateMap<DeleteTreatmentRequest, Treatment>();
        
        CreateMap<UserRequestQueue, User>();
        CreateMap<User, UserRequestQueue>();
    }
}