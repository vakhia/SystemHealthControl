using AutoMapper;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Services;

public class TreatmentService : ITreatmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TreatmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateTreatmentRequest> CreateTreatmentAsync(CreateTreatmentRequest treatmentRequest)
    {
        var treatment = _mapper.Map<CreateTreatmentRequest, Treatment>(treatmentRequest);
        if (treatmentRequest.MedicalExaminationsIds.Count > 0)
        {
            foreach (var id in treatmentRequest.MedicalExaminationsIds)
            {
                var medicalExamination = await _unitOfWork.Repository<MedicalExamination>().GetByIdAsync(id);
                var medicalExaminationTreatments = new MedicalExaminationTreatments()
                {
                    MedicalExamination = medicalExamination,
                    Treatment = treatment
                };
                treatment.Examinations.Add(medicalExaminationTreatments);
            }
        }
        _unitOfWork.Repository<Treatment>().Add(treatment);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return treatmentRequest;
    }

    public async Task<Pagination<TreatmentResponse>> GetTreatmentsAsync(
        PaginationSpecificationParams specificationParams)
    {
        var specification = new TreatmentsWithMedicalExaminationsSpecification(specificationParams);
        var treatments = await _unitOfWork.Repository<Treatment>().ListAsync(specification);
        var countTreatmentsSpecification = new TreatmentsWithFiltersForCountSpecification(specificationParams);
        var data = _mapper.Map<IReadOnlyList<Treatment>, IReadOnlyList<TreatmentResponse>>(treatments);
        var totalItems = await _unitOfWork.Repository<Treatment>().CountAsync(countTreatmentsSpecification);

        return new Pagination<TreatmentResponse>(specificationParams.PageIndex, specificationParams.PageSize,
            totalItems, data);
    }

    public async Task<TreatmentResponse> GetTreatmentByIdAsync(int id)
    {
        var specification = new TreatmentsWithMedicalExaminationsSpecification(id);
        return _mapper.Map<Treatment, TreatmentResponse>( await _unitOfWork.Repository<Treatment>()
            .GetEntityWithSpec(specification));
    }

    public async Task<UpdateTreatmentRequest> UpdateTreatmentAsync(UpdateTreatmentRequest treatmentRequest)
    {
        var treatment = _mapper.Map<UpdateTreatmentRequest, Treatment>(treatmentRequest);
        //TODO Need to add updating many-to-many relationship
        _unitOfWork.Repository<Treatment>().Update(treatment);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return treatmentRequest;
    }

    public async Task<DeleteTreatmentRequest> DeleteTreatmentAsync(DeleteTreatmentRequest treatmentRequest)
    {
        var treatment = _mapper.Map<DeleteTreatmentRequest, Treatment>(treatmentRequest);
        _unitOfWork.Repository<Treatment>().Delete(treatment);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return treatmentRequest;
    }
}