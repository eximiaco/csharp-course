using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Domain.Sinistros;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow
{
    public class CalcularRiscoStep : IStepBody
    {
        private readonly PropostasDbContext _dbContext;
        private readonly CalculadoraRisco _calculadora;
        private readonly IHistoricoAcidentesService _historicoService;

        public Guid CotacaoId { get; set; }

        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var cotacao = await _dbContext.Cotacoes
                .FindAsync(CotacaoId);

            var historico = await _historicoService
                .ObterHistorico(cotacao.Condutor.Cpf);

            var nivelRisco = _calculadora.Calcular(
                cotacao.Condutor.DataNascimento,
                historico,
                cotacao.Condutor.Residencia.UF);

            cotacao.AtualizarRisco(nivelRisco);
            await _dbContext.SaveChangesAsync();

            return ExecutionResult.Next();
        }
    }
} 