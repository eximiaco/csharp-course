using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Agentes;

public sealed class AgentesRepository(PropostasDbContext propostasDbContext)
{
    public async Task<Maybe<Agente>> Obter(string id, CancellationToken cancellationToken = default)
    {
        return (await propostasDbContext.Agentes.FindAsync(id, cancellationToken ))!;
    }
}