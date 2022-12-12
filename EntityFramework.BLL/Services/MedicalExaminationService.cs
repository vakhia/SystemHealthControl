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
        medicalExamination.Appointments = new List<Appointment>();
        _unitOfWork.Repository<MedicalExamination>().Add(medicalExamination);
        //TODO Need to add creating many-to-many relationship with Appointments
        // foreach (var appointmentId in medicalExaminationRequest.AppointmentsIds)
        // {
        //     var appointmentMedicalExamination = new AppointmentMedicalExamination()
        //     {
        //         AppointmentId = appointmentId,
        //         MedicalExaminationId = medicalExamination.Id,
        //     };
        //     _unitOfWork.Repository<AppointmentMedicalExamination>().Add(appointmentMedicalExamination);
        // }

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
            _mapper.Map<UpdateMedicalExaminationRequest, MedicalExamination>(medicalExaminationRequest);
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