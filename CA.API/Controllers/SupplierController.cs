using CA.Application.Suppliers.Commands.CreateSupplier;
using CA.Application.Suppliers.Commands.DeleteSupplier;
using CA.Application.Suppliers.Commands.UpdateSupplier;
using CA.Application.Suppliers.Queries.GetSupplierById;
using CA.Application.Suppliers.Queries.GetSuppliers;
using Microsoft.AspNetCore.Mvc;

namespace CA.API.Controllers;

public class SupplierController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<SuppliersResponse>>> GetSuppliers()
    {
        return await Mediator.Send(new GetSuppliersQuery.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierResponse>> GetSupplierById(Guid id)
    {
        return await Mediator.Send(new GetSupplierByIdQuery.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult> CreateSupplier([FromBody] CreateSupplierRequest createSupplierRequest)
    {
        return Ok(await Mediator.Send(new CreateSupplierCommand.Command()
        {
            CreateSupplierRequest = createSupplierRequest
        }));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateSupplier([FromBody] UpdateSupplierRequest updateSupplierRequest)
    {
        return Ok(await Mediator.Send(new UpdateSupplierCommand.Command
            { UpdateSupplierRequest = updateSupplierRequest }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSupplier(Guid id)
    {
        return Ok(await Mediator.Send(new DeleteSupplierCommand.Command { Id = id }));
    }
}