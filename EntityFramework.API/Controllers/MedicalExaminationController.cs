using EntityFramework.API.Errors;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.API.Controllers;

public class MedicalExaminationController : BaseApiController
{
    private readonly IMedicalExaminationService _medicalExaminationService;

    public MedicalExaminationController(IMedicalExaminationService medicalExaminationService)
    {
        _medicalExaminationService = medicalExaminationService;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<MedicalExaminationResponse>>> GetMedicalExaminations(
        [FromQuery] PaginationSpecificationParams specification)
    {
        var orders = await _medicalExaminationService.GetMedicalExaminationsAsync(specification);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MedicalExaminationResponse>> GetMedicalExaminationById(int id)
    {
        var order = await _medicalExaminationService.GetMedicalExaminationByIdAsync(id);

        if (order == null)
        {
            return NotFound(new ApiResponse(404));
        }

        return order;
    }

    [HttpPost]
    public async Task<ActionResult<CreateMedicalExaminationRequest>> CreateMedicalExamination(
        CreateMedicalExaminationRequest medicalExaminationRequest)
    {
        var result = await _medicalExaminationService.CreateMedicalExaminationAsync(medicalExaminationRequest);

        if (result == null)
        {
            return BadRequest(new ApiResponse(500, "Something went wrong with creating medical examination"));
        }

        return result;
    }
    
    [HttpPut]
    public async Task<ActionResult<UpdateMedicalExaminationRequest>> UpdateMedicalExamination(
        UpdateMedicalExaminationRequest medicalExaminationRequest)
    {
        return await _medicalExaminationService.UpdateMedicalExaminationAsync(medicalExaminationRequest);
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteMedicalExaminationRequest>> DeleteMedicalExamination(
        DeleteMedicalExaminationRequest medicalExaminationRequest)
    {
        return await _medicalExaminationService.DeleteMedicalExaminationAsync(medicalExaminationRequest);
    }
}