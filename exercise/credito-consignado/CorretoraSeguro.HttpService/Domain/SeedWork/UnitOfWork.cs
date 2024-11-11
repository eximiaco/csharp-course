namespace CorretoraSeguro.HttpService.Domain.SeedWork;

public sealed class UnitOfWork(PropostasDbContext context)
{
    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
         return context.SaveChangesAsync(cancellationToken);
    }
}