using CreditoConsignado.HttpService.Domain.Agentes;
using CreditoConsignado.HttpService.Domain.Convenios;
using CreditoConsignado.HttpService.Domain.Propostas.Services;
using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas.Features.NovaProposta;

public class NovaPropostaHandler(
    AgentesRepository agentesRepository,
    ConveniosRepository conveniosRepository,
    PropostaRepository propostaRepository,
    TipoAssinaturaService tipoAssinaturaService)
{
    public async Task<Result<int>> Executar(NovaPropostaCommand command, CancellationToken cancellationToken)
    {
        var agente = await agentesRepository.Obter(command.AgenteId, cancellationToken);
        if (agente.HasNoValue)
            return Result.Failure<int>("Agente inválido");

        var convenio = await conveniosRepository.Obter(command.ConveniadaId, cancellationToken);
        if (convenio.HasNoValue)
            return Result.Failure<int>("Convênio inválido");

        if(await propostaRepository.ProponentePossuiPropostasAbertas(command.Proponente.Cpf, cancellationToken))
            return Result.Failure<int>("Este proponente possui propostas em aberto");

        var bloqueioCpf = await propostaRepository.ObterBloqueiDeCpf(command.Proponente.Cpf, cancellationToken);
        
        var proponente = new Proponente(
            command.Proponente.Cpf,
            new Telefone("", command.Proponente.Telefone),
            new Endereco("", "", "", "", ""),
            bloqueioCpf);

        var credito = new CreditoSolicitado(command.ValorSolicitado, command.QuantidadeParcelas);
        
        var listaUfParaAssinatura = await propostaRepository.ObterUFsDeAssinaturaHibrida(cancellationToken);
        var tipoAssinatura = tipoAssinaturaService.ObterTipoAssinatura(
            proponente,
            listaUfParaAssinatura.ToList());
        
        var regrasCriacaoProposta = await propostaRepository.ObterRegrasCriacaoNovaProposta(command.Proponente.Cpf, cancellationToken);
        
        var id = await propostaRepository.ObterProximoNumeroDeProposta(cancellationToken);
        
        var proposta = Proposta.Criar(id, proponente, credito, tipoAssinatura, regrasCriacaoProposta);
        if(proposta.IsFailure)
            return Result.Failure<int>(proposta.Error);
        
        await propostaRepository.Adicionar(proposta.Value, cancellationToken);
        return Result.Success(proposta.Value.Id);
    }
}