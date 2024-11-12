using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Domain.Sinistros;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco;

public class CalcularRiscoParaCotacaoHandler
{
    private readonly PropostasDbContext _dbContext;
    private readonly CalculadoraRisco _calculadora;
    private readonly IHistoricoAcidentesService _historicoService;

    public CalcularRiscoParaCotacaoHandler(
        PropostasDbContext dbContext,
        CalculadoraRisco calculadora,
        IHistoricoAcidentesService historicoService)
    {
        _dbContext = dbContext;
        _calculadora = calculadora;
        _historicoService = historicoService;
    }

    public async Task ExecuteAsync(CalcularRiscoParaCotacaoCommand command, CancellationToken cancellationToken = default)
    {
        var cotacao = await _dbContext.Cotacoes
            .FindAsync(command.CotacaoId, cancellationToken);

        var historico = await _historicoService
            .ObterHistorico(cotacao.Condutor.Cpf);

        var nivelRisco = _calculadora.Calcular(
            cotacao.Condutor.DataNascimento,
            historico,
            cotacao.Condutor.Residencia.UF);

        cotacao.AtualizarRisco(nivelRisco);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
} 