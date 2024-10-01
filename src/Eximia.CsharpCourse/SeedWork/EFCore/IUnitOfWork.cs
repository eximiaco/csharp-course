namespace Eximia.CsharpCourse.SeedWork.EFCore;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}

