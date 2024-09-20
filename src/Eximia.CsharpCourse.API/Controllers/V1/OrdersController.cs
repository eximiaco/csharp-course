using Eximia.CsharpCourse.API.Models.Requests;
using Eximia.CsharpCourse.API.Models.Responses;
using Eximia.CsharpCourse.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eximia.CsharpCourse.API.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]

public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = request.CreateCommand();
        if (command.IsFailure)
            return BadRequest(new ErrorResponse(command.Error));

        var result = await _mediator.Send(command.Value, cancellationToken);
        if (result.IsFailure)
            return BadRequest(new ErrorResponse(result.Error));

        var response = new { result.Value.Id };
        return CreatedAtAction(nameof(GetOrderStatus), response, response);
    }

    [HttpGet("{id}/status")]
    public async Task<IActionResult> GetOrderStatus([FromServices] OrdersDataAccess ordersDataAccess, int id, CancellationToken cancellationToken)
    {
        var status = await ordersDataAccess.GetStatusByIdAsync(id, cancellationToken);
        if (status.HasNoValue)
            return NotFound();
        return Ok(new { status = status.Value });
    }
}
