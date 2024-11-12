using CorretoraSeguro.HttpService.Domain.SeedWork;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes;

public class CotacoesRepository(PropostasDbContext PropostasDbContext)
{
    public async Task AddAsync(Cotacao cotacao, CancellationToken cancellationToken)
    {
        await PropostasDbContext
            .Cotacoes
            .AddAsync(cotacao, cancellationToken)
            .ConfigureAwait(false);
    }
}