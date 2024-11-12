using CorretoraSeguro.HttpService.Domain.SeedWork;
using WorkflowCore.Exceptions;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularValorFinal;

public class CalcularValorFinalHandler(CotacoesRepository cotacoesRepository, UnitOfWork unitOfWork)
{
    public async Task Handle(CalcularValorFinalCommand command, CancellationToken cancellationToken)
    {
        var cotacao = await cotacoesRepository.ObterPorIdAsync(command.CotacaoId, cancellationToken).ConfigureAwait(false);
        if (cotacao.HasNoValue)
            throw new NotFoundException("Cotação não encontrada.");

        cotacao.Value.CalcularValorFinal();
        await unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
    }
}
