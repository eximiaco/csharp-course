namespace Eximia.CsharpCourse.SeedWork;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}

