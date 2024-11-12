using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco.Regras;
using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Domain.Sinistros;
using WorkflowCore.Exceptions;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco;

public class CalcularRiscoParaCotacaoHandler(
    CotacoesRepository cotacoesRepository,
    CalculadoraRiscoService calculadora,
    UnitOfWork unitOfWork,
    IHistoricoAcidentesService historicoService)
{
    public async Task ExecuteAsync(CalcularRiscoParaCotacaoCommand command, CancellationToken cancellationToken = default)
    {
        var cotacao = await cotacoesRepository.ObterPorIdAsync(command.CotacaoId, cancellationToken).ConfigureAwait(false);
        if (cotacao.HasNoValue)
            throw new NotFoundException("Cotação não encontrada.");

        var historico = await historicoService.ObterHistorico(cotacao.Value.Condutor.Cpf).ConfigureAwait(false);

        var nivelRisco = calculadora.Calcular(
            cotacao.Value.Condutor.DataNascimento,
            historico,
            cotacao.Value.Condutor.Residencia.UF);

        cotacao.Value.AtualizarRisco(nivelRisco);
        await unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
    }
} 