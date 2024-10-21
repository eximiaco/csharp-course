using EscolaEximia.HttpService.Dominio.Entidades;
using EscolaEximia.HttpService.Dominio.Infraestrutura;
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
        [FromServices] InscricoesRepositorio inscricoesRepositorio,
        CancellationToken cancellationToken)
    {
        if (!await inscricoesRepositorio.AlunoExiste(input.CpfAluno))
            return BadRequest("Aluno inválida");
        
        if (!await inscricoesRepositorio.ResponsavelExiste(input.CpfResponsavel))
            return BadRequest("Responsavel inválido");
        
        var turma = await inscricoesRepositorio.RecuperarTurma(input.CodigoTurma, cancellationToken);
        if (turma.HasNoValue)
            return BadRequest("Turma inválida");

        var inscricao = new Inscricao();
        inscricao.Aluno = input.CpfAluno;
        inscricao.Responsavel = input.CpfResponsavel;
        inscricao.Turma = turma.Value.Id;
        inscricao.Ativa = true;
        inscricao.Id = Guid.NewGuid();

        if (turma.Value.Vagas - 1 < 0)
            return BadRequest("Sem Vagas");
        
       var aluno = await inscricoesRepositorio.RecuperarAluno(input.CpfAluno);
       
       if (aluno.Value.Sexo != ESexo.Feminino && turma.Value.Masculino)
           return BadRequest("Turma válida apenas mas sexo Masculino");
       
       if (aluno.Value.Sexo != ESexo.Feminino && turma.Value.Feminino)
           return BadRequest("Turma válida apenas mas sexo Feminio");

       if (aluno.Value.Idade > turma.Value.LimiteIdade)
           return BadRequest("Fora do liimite de idade da turma");
       
        await inscricoesRepositorio.Adicionar(inscricao, cancellationToken);
        await inscricoesRepositorio.Save();

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