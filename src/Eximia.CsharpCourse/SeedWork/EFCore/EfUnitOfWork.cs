namespace Eximia.CsharpCourse.SeedWork.EFCore;

public class EfUnitOfWork : IEfUnitOfWork
{
    private readonly IEFDbContextAccessor<EximiaCsharpCourseDbContext> _efDbContextAccessor;

    public EfUnitOfWork(IEFDbContextAccessor<EximiaCsharpCourseDbContext> efDbContextAccessor)
    {
        _efDbContextAccessor = efDbContextAccessor;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _efDbContextAccessor.Get().SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}