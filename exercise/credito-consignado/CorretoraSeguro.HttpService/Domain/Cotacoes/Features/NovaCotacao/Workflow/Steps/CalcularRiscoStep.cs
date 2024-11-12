using CorretoraSeguro.HttpService.Domain.Sinistros;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow.Steps;

public class CalcularRiscoStep(
    CalculadoraRisco calculadora,
    IHistoricoAcidentesService historicoService,
    CalcularRiscoParaCotacaoHandler calcularRisco)
    : IStepBody
{
    private readonly CalculadoraRisco _calculadora = calculadora;
    private readonly IHistoricoAcidentesService _historicoService = historicoService;

    public Guid CotacaoId { get; set; } = Guid.NewGuid();

    public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var data = context.Workflow.Data as CotacaoData;
        await calcularRisco.ExecuteAsync(new CalcularRiscoParaCotacaoCommand(data!.CotacaoId));
        return ExecutionResult.Next();
    }
}
