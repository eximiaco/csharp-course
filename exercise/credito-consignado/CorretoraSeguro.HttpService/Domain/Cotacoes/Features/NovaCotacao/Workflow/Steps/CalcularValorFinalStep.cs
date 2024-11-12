using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularValorFinal;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow.Steps
{
    public class CalcularValorFinalStep(CalcularValorFinalHandler handler) : IStepBody
    {
        public Guid CotacaoId { get; set; }

        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var command = new CalcularValorFinalCommand(CotacaoId);
            await handler.Handle(command, CancellationToken.None);
            
            return ExecutionResult.Next();
        }
    }
} 