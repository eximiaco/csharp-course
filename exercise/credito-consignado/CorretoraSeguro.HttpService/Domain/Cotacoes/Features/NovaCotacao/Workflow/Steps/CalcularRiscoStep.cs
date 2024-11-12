using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Domain.Sinistros;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow.Steps
{
    public class CalcularRiscoStep : IStepBody
    {
        private readonly PropostasDbContext _dbContext;
        private readonly CalculadoraRisco _calculadora;
        private readonly IHistoricoAcidentesService _historicoService;
        private readonly CalcularRiscoParaCotacaoHandler _calcularRisco;

        public Guid CotacaoId { get; set; } = Guid.NewGuid();
        
        public CalcularRiscoStep(
            PropostasDbContext dbContext,
            CalculadoraRisco calculadora,
            IHistoricoAcidentesService historicoService,
            CalcularRiscoParaCotacaoHandler calcularRisco)
        {
            _dbContext = dbContext;
            _calculadora = calculadora;
            _historicoService = historicoService;
            _calcularRisco = calcularRisco;
        }

        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var data = context.Workflow.Data as CotacaoData;
            await _calcularRisco.ExecuteAsync(new CalcularRiscoParaCotacaoCommand(data.CotacaoId));
            return ExecutionResult.Next();
        }
    }
} 