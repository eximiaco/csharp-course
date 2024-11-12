using CorretoraSeguro.HttpService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CorretoraSeguro.HttpService.Domain.Coberturas;

public class CoberturasRepository(PropostasDbContext PropostasDbContext)
{
    public async Task<IEnumerable<Cobertura>> RecuperarPeloNomeAsync(IEnumerable<string> nomes, CancellationToken cancellationToken)
    {
        return await PropostasDbContext
            .Coberturas
            .Where(c => nomes.Contains(c.Nome))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
