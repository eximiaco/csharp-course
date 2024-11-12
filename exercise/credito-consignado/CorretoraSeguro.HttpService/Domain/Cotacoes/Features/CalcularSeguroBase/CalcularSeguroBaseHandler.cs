using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Domain.TabelaFipe;
using WorkflowCore.Exceptions;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularSeguroBase;

public class CalcularSeguroBaseHandler(CotacoesRepository cotacoesRepository, UnitOfWork unitOfWork, IFipeService fipeService)
{
    public async Task ExecuteAsync(CalcularSeguroBaseCommand command, CancellationToken cancellationToken = default)
    {
        var cotacao = await cotacoesRepository.ObterPorIdAsync(command.CotacaoId, cancellationToken).ConfigureAwait(false);
        if (cotacao.HasNoValue)
            throw new NotFoundException("Cotação não encontrada.");

        var valorMercado = await fipeService.ObterValorAsync(
            cotacao.Value.Veiculo.Marca,
            cotacao.Value.Veiculo.Modelo,
            cotacao.Value.Veiculo.Ano).ConfigureAwait(false);

        cotacao.Value.AtualizarValorBase(valorMercado);
        await unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
    }
} 