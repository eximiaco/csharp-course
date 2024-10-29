using EscolaEximia.HttpService.Dominio.Inscricoes.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace EscolaEximia.HttpService.Controllers;

[ApiController]
//[Authorize()]
[Route("api/v{version:apiVersion}/{controller}")]
[ApiVersion("1.0")]
public sealed class InscricoesController : ControllerBase
{
    public record NovaInscricaoModel(string CpfAluno, string CpfResponsavel, int CodigoTurma);
    
    [HttpPost]
    public async Task<IActionResult> RealizarInscricao(
        [FromBody] NovaInscricaoModel input, 
        [FromServices] RealizarInscricaoHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new RealizarInscricaoCommand(
            Aluno: input.CpfAluno,
            Responsavel: input.CpfResponsavel,
            Turma: input.CodigoTurma
        );

        var result = await handler.Handle(command, cancellationToken);

        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarInscricao(
        string id, 
        CancellationToken cancellationToken)
    {
        
        return Ok();
    }
}
