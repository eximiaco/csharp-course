using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Domain.Sinistros;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco;

public class CalcularRiscoParaCotacaoHandler(
    PropostasDbContext dbContext,
    CalculadoraRisco calculadora,
    IHistoricoAcidentesService historicoService)
{
    public async Task ExecuteAsync(CalcularRiscoParaCotacaoCommand command, CancellationToken cancellationToken = default)
    {
        var cotacao = await dbContext.Cotacoes
            .FindAsync(command.CotacaoId, cancellationToken);

        var historico = await historicoService
            .ObterHistorico(cotacao.Condutor.Cpf);

        var nivelRisco = calculadora.Calcular(
            cotacao.Condutor.DataNascimento,
            historico,
            cotacao.Condutor.Residencia.UF);

        cotacao.AtualizarRisco(nivelRisco);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 