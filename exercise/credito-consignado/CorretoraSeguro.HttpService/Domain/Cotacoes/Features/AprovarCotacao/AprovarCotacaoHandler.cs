using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow;
using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;
using WorkflowCore.Interface;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.AprovarCotacao
{
    public class AprovarCotacaoHandler
    {
        private readonly PropostasDbContext _dbContext;
        private readonly IWorkflowHost _workflowHost;

        public AprovarCotacaoHandler(
            PropostasDbContext dbContext,
            IWorkflowHost workflowHost)
        {
            _dbContext = dbContext;
            _workflowHost = workflowHost;
        }

        public async Task<Result<string>> Handle(Guid cotacaoId, CancellationToken cancellationToken)
        {
            var cotacao = await _dbContext.Cotacoes
                .FindAsync(cotacaoId);

            if (cotacao == null)
                return Result.Failure<string>("Cotação não encontrada");

            if (cotacao.Status != StatusCotacao.AguardandoAprovacao)
                return Result.Failure<string>("Cotação não está aguardando aprovação");

            cotacao.Aprovar();
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Publicar evento do workflow
            await _workflowHost.PublishEvent("cotacao-aprovada", cotacao.Id.ToString(), null);

            return Result.Success("Cotação aprovada com sucesso");
        }
    }
} 