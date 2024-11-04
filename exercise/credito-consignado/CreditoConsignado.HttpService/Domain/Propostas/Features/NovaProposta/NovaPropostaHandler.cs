using CreditoConsignado.HttpService.Domain.Agentes;
using CreditoConsignado.HttpService.Domain.Convenios;
using CreditoConsignado.HttpService.Domain.Propostas.Services;
using CreditoConsignado.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas.Features.NovaProposta;

public class NovaPropostaHandler(
    AgentesRepository agentesRepository,
    ConveniosRepository conveniosRepository,
    PropostaRepository propostaRepository,
    TipoAssinaturaService tipoAssinaturaService,
    UnitOfWork unitOfWork)
{
    public async Task<Result<int>> ExecutarAsync(NovaPropostaCommand command, CancellationToken cancellationToken)
    {
        var agente = await agentesRepository.ObterAsync(command.AgenteId, cancellationToken).ConfigureAwait(false);
        if (agente.HasNoValue)
            return Result.Failure<int>("Agente inválido.");

        var convenio = await conveniosRepository.ObterAsync(command.ConveniadaId, cancellationToken).ConfigureAwait(false);
        if (convenio.HasNoValue)
            return Result.Failure<int>("Convênio inválido.");

        if(await propostaRepository.ProponentePossuiPropostasAbertasAsync(command.Proponente.Cpf, cancellationToken).ConfigureAwait(false))
            return Result.Failure<int>("Este proponente possui propostas em aberto.");

        var bloqueioCpf = await propostaRepository.ObterBloqueioDeCpfAsync(command.Proponente.Cpf, cancellationToken).ConfigureAwait(false);        
        var proponente = new Proponente(
            command.Proponente.Cpf,
            new Telefone("", command.Proponente.Telefone),
            new Endereco("", "", "", "", ""),
            bloqueioCpf);

        var credito = new CreditoSolicitado(command.ValorSolicitado, command.QuantidadeParcelas);
        
        var listaUfParaAssinatura = await propostaRepository.ObterUFsDeAssinaturaHibridaAsync(cancellationToken).ConfigureAwait(false);
        var tipoAssinatura = tipoAssinaturaService.ObterTipoAssinatura(proponente, listaUfParaAssinatura.ToList());        
        var regrasCriacaoProposta = await propostaRepository.ObterRegrasCriacaoNovaPropostaAsync(command.Proponente.Cpf, cancellationToken).ConfigureAwait(false);    
        var id = await propostaRepository.ObterProximoNumeroDeProposta(cancellationToken).ConfigureAwait(false);
        
        var proposta = Proposta.Criar(
            id,
            convenio.Value,
            agente.Value,
            proponente,
            credito,
            tipoAssinatura,
            regrasCriacaoProposta);
        if(proposta.IsFailure)
            return Result.Failure<int>(proposta.Error);
        
        await propostaRepository.Adicionar(proposta.Value, cancellationToken);
        
        await unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Success(proposta.Value.Id);
    }
}