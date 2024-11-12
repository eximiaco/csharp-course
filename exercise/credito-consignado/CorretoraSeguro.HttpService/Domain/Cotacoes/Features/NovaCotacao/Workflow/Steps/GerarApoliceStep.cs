using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow.Steps;

public class GerarApoliceStep : IStepBody
{
    public Guid CotacaoId { get; set; }

    public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        // Implementar a lógica de geração da apólice aqui
        return ExecutionResult.Next();
    }
} 