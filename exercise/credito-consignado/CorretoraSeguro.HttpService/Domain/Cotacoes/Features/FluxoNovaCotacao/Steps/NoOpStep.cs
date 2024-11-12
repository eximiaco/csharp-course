using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.FluxoNovaCotacao.Steps;

public class NoOpStep : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        return ExecutionResult.Next();
    }
}