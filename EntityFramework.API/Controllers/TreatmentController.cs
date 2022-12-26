using EntityFramework.API.Errors;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.API.Controllers;

public class TreatmentController : BaseApiController
{
    private readonly ITreatmentService _treatmentService;
    private readonly ILogger<TreatmentController> _logger;

    public TreatmentController(ITreatmentService treatmentService, ILogger<TreatmentController> logger)
    {
        _treatmentService = treatmentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<TreatmentResponse>>> GetTreatments(
        [FromQuery] PaginationSpecificationParams specificationParams)
    {
        try
        {
            return Ok(await _treatmentService.GetTreatmentsAsync(specificationParams));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TreatmentResponse>> GetTreatment(int id)
    {
        try
        {
            return Ok(await _treatmentService.GetTreatmentByIdAsync(id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [HttpPost]
    public async Task<ActionResult<CreateTreatmentRequest>> CreateTreatment(CreateTreatmentRequest treatmentRequest)
    {
        try
        {
            return Ok(await _treatmentService.CreateTreatmentAsync(treatmentRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [HttpPut]
    public async Task<ActionResult<UpdateTreatmentRequest>> UpdateTreatment(UpdateTreatmentRequest treatmentRequest)
    {
        try
        {
            return Ok(await _treatmentService.UpdateTreatmentAsync(treatmentRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteTreatmentRequest>> DeleteTreatment(DeleteTreatmentRequest treatmentRequest)
    {
        try
        {
            return Ok(await _treatmentService.DeleteTreatmentAsync(treatmentRequest));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new ApiResponse(500));
        }
    }
}