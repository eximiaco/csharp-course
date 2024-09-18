using Eximia.CsharpCourse.SeedWork.EFCore;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eximia.CsharpCourse.SeedWork.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest
{
    private readonly ILogger<UnitOfWorkBehavior<TRequest, TResponse>> _logger;
    private readonly IEFDbContextFactory<EximiaCsharpCourseDbContext> _efDbContextFactory;
    private readonly IEFDbContextAccessor<EximiaCsharpCourseDbContext> _efDbContextAccessor;

    public UnitOfWorkBehavior(
        ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger,
        IEFDbContextFactory<EximiaCsharpCourseDbContext> efDbContextFactory,
        IEFDbContextAccessor<EximiaCsharpCourseDbContext> efDbContextAccessor)
    {
        _logger = logger;
        _efDbContextFactory = efDbContextFactory;
        _efDbContextAccessor = efDbContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.GetType().GetInterfaces().All(c => c != typeof(ICommand)))
            return await next();

        try
        {
            await using var context = _efDbContextFactory.Create();
            _efDbContextAccessor.Register(context);
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Não foi possível processar o comando.");
            throw;
        }

    }
}
