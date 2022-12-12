using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Interfaces;

public interface IMedicalExaminationService
{
    Task<CreateMedicalExaminationRequest> CreateMedicalExaminationAsync(CreateMedicalExaminationRequest medicalExaminationRequest);
    Task<Pagination<MedicalExaminationResponse>> GetMedicalExaminationsAsync(PaginationSpecificationParams specification);
    Task<MedicalExaminationResponse> GetMedicalExaminationByIdAsync(int id);
    Task<UpdateMedicalExaminationRequest> UpdateMedicalExaminationAsync(UpdateMedicalExaminationRequest medicalExaminationRequest);
    Task<DeleteMedicalExaminationRequest> DeleteMedicalExaminationAsync(DeleteMedicalExaminationRequest medicalExaminationRequest);
}