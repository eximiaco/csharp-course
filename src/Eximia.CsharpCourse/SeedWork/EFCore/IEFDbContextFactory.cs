using Microsoft.EntityFrameworkCore;

namespace Eximia.CsharpCourse.SeedWork.EFCore;

public interface IEFDbContextFactory<T> where T : DbContext
{
    T Create();
}
