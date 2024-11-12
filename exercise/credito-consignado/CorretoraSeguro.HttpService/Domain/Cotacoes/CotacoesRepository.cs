using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Maybe<Cotacao>> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cotacao = await PropostasDbContext
            .Cotacoes
            .Include(x => x.Coberturas)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            .ConfigureAwait(false);

        if (cotacao is null)
            return Maybe.None;
        return cotacao;
    }
}