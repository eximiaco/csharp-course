using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow.Steps;

public class CalcularBaseSeguroStep : IStepBody
{
    public Guid CotacaoId { get; set; } = Guid.NewGuid();

    public Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        // Implementar lógica de cálculo do seguro base aqui
        return Task.FromResult(ExecutionResult.Next());
    }
} 