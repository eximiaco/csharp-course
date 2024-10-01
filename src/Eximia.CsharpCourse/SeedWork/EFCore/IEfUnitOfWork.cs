namespace Eximia.CsharpCourse.SeedWork.EFCore;

public interface IEfUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}
