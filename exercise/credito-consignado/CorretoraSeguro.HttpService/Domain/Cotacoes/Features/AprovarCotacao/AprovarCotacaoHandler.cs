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

        var resultadoAprovacao = cotacao.Value.Aprovar();
        if (resultadoAprovacao.IsFailure)
            return Result.Failure<string>(resultadoAprovacao.Error);

        await unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

        // Publicar evento do workflow
        await workflowHost.PublishEvent("cotacao-aprovada", cotacao.Value.Id.ToString(), null);
        return Result.Success("Cotação aprovada com sucesso");
    }
}
