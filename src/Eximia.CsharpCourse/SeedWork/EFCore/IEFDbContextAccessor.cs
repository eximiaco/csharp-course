using Microsoft.EntityFrameworkCore;

namespace Eximia.CsharpCourse.SeedWork.EFCore;

public interface IEFDbContextAccessor<T> : IDisposable, IAsyncDisposable where T : DbContext
{
    void Register(T context);
    T Get();
    Task ClearAsync();
}
