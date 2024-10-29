using CreditoConsignado.HttpService.Domain.Propostas.Features.NovaProposta;
using Microsoft.AspNetCore.Mvc;

namespace CreditoConsignado.HttpService.Controller;

[ApiController]
//[Authorize()]
[Route("api/v{version:apiVersion}/{controller}")]
[ApiVersion("1.0")]
public class PropostasController : ControllerBase
{
    public record NovaProposta(
        string Cpf,
        string ConveniadaId,
        decimal ValorSolicitado,
        int QuantidadeParcelas,
        string AgenteId,
        DateTime DataNascimento,
        string Endereco,
        string UF,
        string Telefone,
        string Email,
        decimal Rendimento
    );
    
    [HttpPost]
    public async Task<IActionResult> CriarProposta(
        [FromBody] NovaProposta input, 
        [FromServices] NovaPropostaHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new NovaPropostaCommand(
            input.ConveniadaId,
            input.ValorSolicitado,
            input.QuantidadeParcelas,
            input.AgenteId,
            new DadosProponente(input.Cpf,
                input.DataNascimento, 
                input.Endereco, 
                input.UF, 
                input.Telefone, 
                input.Email, 
                input.Rendimento)
        );

        var result = await handler.ExecutarAsync(command, cancellationToken);

        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Error);
    }
}