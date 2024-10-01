using Eximia.CsharpCourse.SeedWork.EFCore;

namespace Eximia.CsharpCourse.API.Infrastructure;

public class UnitOfWorkMiddleware(
    IEFDbContextFactory<EximiaCsharpCourseDbContext> factory,
    IEFDbContextAccessor<EximiaCsharpCourseDbContext> accessor)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await using var dbContext = factory.Create();
        accessor.Register(dbContext);
        // Call the next delegate/middleware in the pipeline.
        await next(context);
        await accessor.ClearAsync();
    }
}

public static class UnitOfWorkMiddlewareMiddlewareExtensions
{
    public static IApplicationBuilder UseUnitOfWork(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UnitOfWorkMiddleware>();
    }
}