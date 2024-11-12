using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularSeguroBase;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow.Steps;

public class CalcularBaseSeguroStep(CalcularSeguroBaseHandler handler) : IStepBody
{
    public Guid CotacaoId { get; set; } = Guid.NewGuid();

    public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var command = new CalcularSeguroBaseCommand(CotacaoId);
        await handler.ExecuteAsync(command);
        
        return ExecutionResult.Next();
    }
} 