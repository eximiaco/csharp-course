using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Domain.TabelaFipe;
using Microsoft.EntityFrameworkCore;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularSeguroBase;

public class CalcularSeguroBaseHandler
{
    private readonly PropostasDbContext _dbContext;
    private readonly IFipeService _fipeService;

    public CalcularSeguroBaseHandler(
        PropostasDbContext dbContext,
        IFipeService fipeService)
    {
        _dbContext = dbContext;
        _fipeService = fipeService;
    }

    public async Task ExecuteAsync(CalcularSeguroBaseCommand command, CancellationToken cancellationToken = default)
    {
        var cotacao = await _dbContext.Cotacoes
            .Include(c => c.Veiculo)
            .Include(c=> c.Coberturas)
            .FirstOrDefaultAsync(c=> c.Id == command.CotacaoId, cancellationToken);

        var valorMercado = await _fipeService.ObterValorAsync(
            cotacao.Veiculo.Marca,
            cotacao.Veiculo.Modelo,
            cotacao.Veiculo.Ano);

        foreach (var tipo in cotacao.Coberturas.Select(c => c.Tipo))
        {
            cotacao.AdicionarCobertura(tipo, valorMercado);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
} 