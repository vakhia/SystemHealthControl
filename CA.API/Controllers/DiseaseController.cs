using CA.Application.Diseases.Commands.CreateDisease;
using CA.Application.Diseases.Commands.DeleteDisease;
using CA.Application.Diseases.Commands.UpdateDisease;
using CA.Application.Diseases.Queries.GetDiseaseById;
using CA.Application.Diseases.Queries.GetDiseases;
using Microsoft.AspNetCore.Mvc;

namespace CA.API.Controllers;

public class DiseaseController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<DiseasesResponse>>> GetDiseases()
    {
        return await Mediator.Send(new GetDiseasesQuery.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DiseaseResponse>> GetDiseaseById(Guid id)
    {
        return await Mediator.Send(new GetDiseaseByIdQuery.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult> CreateDisease([FromBody] CreateDiseaseRequest createDiseaseRequest)
    {
        return Ok(await Mediator.Send(new CreateDiseaseCommand.Command
            { CreateDiseaseRequest = createDiseaseRequest }));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateDisease([FromBody] UpdateDiseaseRequest updateDiseaseRequest)
    {
        return Ok(await Mediator.Send(new UpdateDiseaseCommand.Command
            { UpdateDiseaseRequest = updateDiseaseRequest }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDisease(Guid id)
    {
        return Ok(await Mediator.Send(new DeleteDiseaseCommand.Command { Id = id }));
    }
}