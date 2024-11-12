using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Domain.TabelaFipe;
using Microsoft.EntityFrameworkCore;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularSeguroBase;

public class CalcularSeguroBaseHandler(
    PropostasDbContext dbContext,
    IFipeService fipeService)
{
    public async Task ExecuteAsync(CalcularSeguroBaseCommand command, CancellationToken cancellationToken = default)
    {
        var cotacao = await dbContext.Cotacoes
            .Include(c => c.Veiculo)
            .Include(c=> c.Coberturas)
            .FirstOrDefaultAsync(c=> c.Id == command.CotacaoId, cancellationToken);

        var valorMercado = await fipeService.ObterValorAsync(
            cotacao.Veiculo.Marca,
            cotacao.Veiculo.Modelo,
            cotacao.Veiculo.Ano);

        foreach (var tipo in cotacao.Coberturas.Select(c => c.Tipo))
        {
            cotacao.AdicionarCobertura(tipo, valorMercado);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 