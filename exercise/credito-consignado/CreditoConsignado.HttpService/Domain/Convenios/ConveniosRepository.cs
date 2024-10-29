using CreditoConsignado.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Convenios;

public sealed class ConveniosRepository(PropostasDbContext propostasDbContext)
{
    public async Task<Maybe<Convenio>> ObterAsync(string id, CancellationToken cancellationToken = default)
    {
        var convenio = await propostasDbContext.Convenios.FindAsync(id, cancellationToken).ConfigureAwait(false);
        if (convenio is null)
            return Maybe.None;
        return convenio;
    }
}