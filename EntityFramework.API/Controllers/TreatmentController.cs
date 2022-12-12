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

    public TreatmentController(ITreatmentService treatmentService)
    {
        _treatmentService = treatmentService;
    }

    [HttpGet]
    public async Task<Pagination<TreatmentResponse>> GetTreatments(
        [FromQuery] PaginationSpecificationParams specificationParams)
    {
        var orders = await _treatmentService.GetTreatmentsAsync(specificationParams);
        return orders;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TreatmentResponse>> GetTreatment(int id)
    {
        return await _treatmentService.GetTreatmentByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<CreateTreatmentRequest>> CreateTreatment(CreateTreatmentRequest treatmentRequest)
    {
        return await _treatmentService.CreateTreatmentAsync(treatmentRequest);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateTreatmentRequest>> UpdateTreatment(UpdateTreatmentRequest treatmentRequest)
    {
        return await _treatmentService.UpdateTreatmentAsync(treatmentRequest);
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteTreatmentRequest>> DeleteTreatment(DeleteTreatmentRequest treatmentRequest)
    {
        return await _treatmentService.DeleteTreatmentAsync(treatmentRequest);
    }
}