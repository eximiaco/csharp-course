using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.FluxoNovaCotacao.Steps;

public class AtualizarAprovacaoStep : IStepBody
{
    public Guid CotacaoId { get; set; }

    public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        if (context.Workflow.Data is CotacaoData data)
        {
            data.Aprovado = true;
        }
        
        return ExecutionResult.Next();
    }
} 