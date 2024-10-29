using CreditoConsignado.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Agentes;

public sealed class AgentesRepository(PropostasDbContext PropostasDbContext)
{
    public async Task<Maybe<Agente>> ObterAsync(string id, CancellationToken cancellationToken = default)
    {
        var agente = await PropostasDbContext.Agentes.FindAsync(id, cancellationToken).ConfigureAwait(false);
        if (agente is null)
            return Maybe.None;
        return agente;
    }
}