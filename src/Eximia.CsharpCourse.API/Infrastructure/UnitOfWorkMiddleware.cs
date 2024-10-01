using Eximia.CsharpCourse.SeedWork.EFCore;

namespace Eximia.CsharpCourse.API.Infrastructure;

public class UnitOfWorkMiddleware : IMiddleware
{
    private readonly IEFDbContextFactory<EximiaCsharpCourseDbContext> _efDbContextFactory;
    private readonly IEFDbContextAccessor<EximiaCsharpCourseDbContext> _efDbContextAccessor;

    public UnitOfWorkMiddleware(
        IEFDbContextFactory<EximiaCsharpCourseDbContext> efDbContextFactory,
        IEFDbContextAccessor<EximiaCsharpCourseDbContext> efDbContextAccessor)
    {
        _efDbContextFactory = efDbContextFactory;
        _efDbContextAccessor = efDbContextAccessor;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await using var dbContext = _efDbContextFactory.Create();
        _efDbContextAccessor.Register(dbContext);

        // Call the next delegate/middleware in the pipeline
        await next(context);

        await _efDbContextAccessor.ClearAsync();
    }
}
