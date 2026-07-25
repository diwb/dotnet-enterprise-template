using Application.Customers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Pagination;

namespace Presentation.Controllers;

[ApiController]
[Authorize(Policy = "CanManageOrders")]
[Route("api/customers")]
public sealed class CustomersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryParameters parameters, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomersQuery(parameters), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value)
            : BadRequest(new { result.Error.Code, result.Error.Message });
    }
}
