using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;
using WorkflowCore.Interface;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.AprovarCotacao;

public class AprovarCotacaoHandler(CotacoesRepository cotacoesRepository, UnitOfWork unitOfWork, IWorkflowHost workflowHost)
{
    public async Task<Result<string>> Handle(AprovarCotacaoCommand command, CancellationToken cancellationToken)
    {
        var cotacao = await cotacoesRepository.ObterPorIdAsync(command.CotacaoId, cancellationToken).ConfigureAwait(false);
        if (cotacao.HasNoValue)
            return Result.Failure<string>("Cotação não encontrada");

        var resultado = cotacao.Value.Aprovar();
        if (resultado.IsFailure)
            return Result.Failure<string>(resultado.Error);

        await unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        await workflowHost.PublishEvent("AprovacaoCotacao", command.CotacaoId.ToString(), null).ConfigureAwait(false);
        return Result.Success("Cotação aprovada com sucesso");
    }
}
