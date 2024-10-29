using CreditoConsignado.HttpService.Domain.Propostas.Features.NovaProposta;
using Microsoft.AspNetCore.Mvc;

namespace CreditoConsignado.HttpService.Controller;

[ApiController]
//[Authorize()]
[Route("api/v{version:apiVersion}/{controller}")]
[ApiVersion("1.0")]
public class PropostasController : ControllerBase
{
    public record NovaProposta();
    
    [HttpPost]
    public async Task<IActionResult> RealizarInscricao(
        [FromBody] NovaProposta input, 
        [FromServices] NovaPropostaHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new NovaPropostaCommand(
        );

        var result = await handler.Executar(command, cancellationToken);

        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Error);
    }
}