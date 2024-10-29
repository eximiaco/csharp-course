using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Convenios;

public sealed class ConveniosRepository(PropostasDbContext propostasDbContext)
{
    public async Task<Maybe<Convenio>> Obter(string id, CancellationToken cancellationToken = default)
    {
        return (await propostasDbContext.Convenios.FindAsync(id, cancellationToken ))!;
    }
}