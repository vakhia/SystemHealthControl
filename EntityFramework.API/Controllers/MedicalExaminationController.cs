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
    private readonly ILogger<MedicalExaminationController> _logger;

    public MedicalExaminationController(IMedicalExaminationService medicalExaminationService,
        ILogger<MedicalExaminationController> logger)
    {
        _medicalExaminationService = medicalExaminationService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<MedicalExaminationResponse>>> GetMedicalExaminations(
        [FromQuery] PaginationSpecificationParams specification)
    {
        try
        {
            return Ok(await _medicalExaminationService.GetMedicalExaminationsAsync(specification));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MedicalExaminationResponse>> GetMedicalExaminationById(int id)
    {
        try
        {
            var result = await _medicalExaminationService.GetMedicalExaminationByIdAsync(id);

            if (result == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [HttpPost]
    public async Task<ActionResult<CreateMedicalExaminationRequest>> CreateMedicalExamination(
        CreateMedicalExaminationRequest medicalExaminationRequest)
    {
        try
        {
            var result = await _medicalExaminationService.CreateMedicalExaminationAsync(medicalExaminationRequest);

            if (result == null)
            {
                return BadRequest(new ApiResponse(500, "Something went wrong with creating medical examination"));
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [HttpPut]
    public async Task<ActionResult<UpdateMedicalExaminationRequest>> UpdateMedicalExamination(
        UpdateMedicalExaminationRequest medicalExaminationRequest)
    {
        try
        {
            return Ok(await _medicalExaminationService.UpdateMedicalExaminationAsync(medicalExaminationRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteMedicalExaminationRequest>> DeleteMedicalExamination(
        DeleteMedicalExaminationRequest medicalExaminationRequest)
    {
        try
        {
            return Ok(await _medicalExaminationService.DeleteMedicalExaminationAsync(medicalExaminationRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }
}