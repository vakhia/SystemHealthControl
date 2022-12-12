using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Specifications;

namespace EntityFramework.BLL.Interfaces;

public interface ITreatmentService
{
    Task<CreateTreatmentRequest> CreateTreatmentAsync(CreateTreatmentRequest medicalExaminationRequest);
    Task<Pagination<TreatmentResponse>> GetTreatmentsAsync(PaginationSpecificationParams specificationParams);
    Task<TreatmentResponse> GetTreatmentByIdAsync(int id);
    Task<UpdateTreatmentRequest> UpdateTreatmentAsync(UpdateTreatmentRequest medicalExaminationRequest);
    Task<DeleteTreatmentRequest> DeleteTreatmentAsync(DeleteTreatmentRequest medicalExaminationRequest);
}