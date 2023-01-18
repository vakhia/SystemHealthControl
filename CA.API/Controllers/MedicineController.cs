using CA.Application.Medicines.Commands.CreateMedicine;
using CA.Application.Medicines.Commands.DeleteMedicine;
using CA.Application.Medicines.Commands.UpdateMedicine;
using CA.Application.Medicines.Queries.GetMedicineById;
using CA.Application.Medicines.Queries.GetMedicines;
using CA.Domain.Medicines;
using Microsoft.AspNetCore.Mvc;

namespace CA.API.Controllers;

public class MedicineController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<MedicinesResponse>>> GetMedicines()
    {
        return await Mediator.Send(new GetMedicinesQuery.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MedicineResponse>> GetMedicineById(Guid id)
    {
        return await Mediator.Send(new GetMedicineByIdQuery.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult> CreateMedicine([FromBody] CreateMedicineRequest createMedicineRequest)
    {
        return Ok(await Mediator.Send(new CreateMedicineCommand.Command
            { CreateMedicineRequest = createMedicineRequest }));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateMedicine([FromBody] UpdateMedicineRequest updateMedicineRequest)
    {
        return Ok(await Mediator.Send(new UpdateMedicineCommand.Command { UpdateMedicineRequest = updateMedicineRequest }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMedicine(Guid id)
    {
        return Ok(await Mediator.Send(new DeleteMedicineCommand.Command { Id = id }));
    }
}