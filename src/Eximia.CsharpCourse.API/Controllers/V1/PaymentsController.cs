using Eximia.CsharpCourse.API.Models.Responses;
using Eximia.CsharpCourse.Payments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eximia.CsharpCourse.API.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    [HttpPatch("{id}/Refund")]
    public async Task<IActionResult> RefundPayment([FromServices] IMediator mediator, int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefundPaymentCommand(id), cancellationToken);
        if (result.IsFailure)
            return BadRequest(new ErrorResponse(result.Error));
        return Ok();
    }
}
