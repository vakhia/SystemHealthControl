using AutoMapper;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Services;

public class MedicalExaminationService : IMedicalExaminationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicalExaminationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateMedicalExaminationRequest> CreateMedicalExaminationAsync(
        CreateMedicalExaminationRequest medicalExaminationRequest)
    {
        var medicalExamination =
            _mapper.Map<CreateMedicalExaminationRequest, MedicalExamination>(medicalExaminationRequest);
        medicalExamination.Appointments = new List<AppointmentMedicalExamination>();
        if (medicalExaminationRequest.AppointmentsIds.Count > 0)
        {
            foreach (var id in medicalExaminationRequest.AppointmentsIds)
            {
                var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
                var appointmentMedicalExamination = new AppointmentMedicalExamination()
                {
                    Appointment = appointment,
                    MedicalExamination = medicalExamination
                };
                medicalExamination.Appointments.Add(appointmentMedicalExamination);
            }
        }

        _unitOfWork.Repository<MedicalExamination>().Add(medicalExamination);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return medicalExaminationRequest;
    }

    public async Task<Pagination<MedicalExaminationResponse>> GetMedicalExaminationsAsync(
        PaginationSpecificationParams specificationParams)
    {
        var specification = new MedicalExaminationsWithAppointmentSpecification(specificationParams);
        var medicalExaminations = await _unitOfWork.Repository<MedicalExamination>().ListAsync(specification);
        var countMedicalExaminationsSpecification =
            new MedicalExaminationsWithFiltersForCountSpecification(specificationParams);
        var data = _mapper.Map<IReadOnlyList<MedicalExamination>, IReadOnlyList<MedicalExaminationResponse>>(
            medicalExaminations);
        var totalItems = await _unitOfWork.Repository<MedicalExamination>()
            .CountAsync(countMedicalExaminationsSpecification);

        return new Pagination<MedicalExaminationResponse>(specificationParams.PageIndex, specificationParams.PageSize,
            totalItems, data);
    }

    public async Task<MedicalExaminationResponse> GetMedicalExaminationByIdAsync(int id)
    {
        var specification = new MedicalExaminationsWithAppointmentSpecification(id);
        return _mapper.Map<MedicalExamination, MedicalExaminationResponse>(
            await _unitOfWork.Repository<MedicalExamination>().GetEntityWithSpec(specification));
    }

    public async Task<UpdateMedicalExaminationRequest> UpdateMedicalExaminationAsync(
        UpdateMedicalExaminationRequest medicalExaminationRequest)
    {
        var medicalExamination =
            await _unitOfWork.Repository<MedicalExamination>().GetByIdAsync(medicalExaminationRequest.Id);
        //TODO Need to add updating many-to-many relationship
        // var updatedMedicalExamination =
        //     _mapper.Map<UpdateMedicalExaminationRequest, MedicalExamination>(medicalExaminationRequest);
        _unitOfWork.Repository<MedicalExamination>().Update(medicalExamination);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return medicalExaminationRequest;
    }

    public async Task<DeleteMedicalExaminationRequest> DeleteMedicalExaminationAsync(
        DeleteMedicalExaminationRequest medicalExaminationRequest)
    {
        var medicalExamination =
            _mapper.Map<DeleteMedicalExaminationRequest, MedicalExamination>(medicalExaminationRequest);
        _unitOfWork.Repository<MedicalExamination>().Delete(medicalExamination);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return medicalExaminationRequest;
    }
}