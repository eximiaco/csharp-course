using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.FluxoNovaCotacao.Steps;

public class CancelarCotacaoStep : IStepBody
{
    public Guid CotacaoId { get; set; }

    public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        // Implemente aqui a lógica para cancelar a cotação
        return ExecutionResult.Next();
    }
} 