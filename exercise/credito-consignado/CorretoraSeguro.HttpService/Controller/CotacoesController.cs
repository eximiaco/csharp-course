using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.AprovarCotacao;
using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao;
using Microsoft.AspNetCore.Mvc;

namespace CorretoraSeguro.HttpService.Controller;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CotacoesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> NovaCotacao(
        [FromBody] NovaCotacaoRequest request,
        [FromServices] NovaCotacaoHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new NovaCotacaoCommand(
            new NovaCotacaoCommand.VeiculoRecord(
                request.Veiculo.Marca,
                request.Veiculo.Modelo,
                request.Veiculo.Ano),
            new NovaCotacaoCommand.ProprietarioRecord(
                request.Proprietario.Cpf,
                request.Proprietario.Nome,
                request.Proprietario.DataNascimento,
                new NovaCotacaoCommand.EnderecoRecord(
                    request.Proprietario.Residencia.Cep,
                    request.Proprietario.Residencia.Cidade,
                    request.Proprietario.Residencia.UF)),
            new NovaCotacaoCommand.CondutorRecord(
                request.Condutor.Cpf,
                request.Condutor.DataNascimento,
                new NovaCotacaoCommand.EnderecoRecord(
                    request.Condutor.Residencia.Cep,
                    request.Condutor.Residencia.Cidade,
                    request.Condutor.Residencia.UF)),
            request.Coberturas);

        var result = await handler.Handle(command, cancellationToken);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Error);
    }

    [HttpPost("{cotacaoId}/aprovar")]
    public async Task<IActionResult> AprovarCotacao(
        [FromRoute] Guid cotacaoId,
        [FromServices] AprovarCotacaoHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new AprovarCotacaoCommand(cotacaoId), cancellationToken);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Error);
    }
}

public record NovaCotacaoRequest(
    DadosVeiculo Veiculo,
    DadosProprietario Proprietario,
    DadosCondutor Condutor,
    List<string> Coberturas
);

public record DadosVeiculo(string Marca, string Modelo, int Ano);
public record DadosProprietario(string Cpf, string Nome, DateTime DataNascimento, Endereco Residencia);
public record DadosCondutor(string Cpf, DateTime DataNascimento, Endereco Residencia);
public record Endereco(string Cep, string Cidade, string UF);