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
        [FromBody]NovaInscricaoModel input, 
        CancellationToken cancellationToken)
    {
        

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarInscricao(
        string id, 
        CancellationToken cancellationToken)
    {
        
        return Ok();
    }
}