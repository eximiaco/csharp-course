
using Eximia.CsharpCourse.Orders.Commands;
using MediatR;

namespace Eximia.CsharpCourse.Workers.Jobs;

public class GenerateYesterdayOrdersReportJob : BackgroundService
{
    private readonly IServiceScopeFactory _factory;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromHours(1));

    public GenerateYesterdayOrdersReportJob(IServiceScopeFactory factory)
    {
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && await _timer.WaitForNextTickAsync(stoppingToken))
        {
            if (DateTime.Now.Hour != 0)
                continue;

            // We cannot use the default dependency injection behavior, because ExecuteAsync is
            // a long-running method while the background service is running.
            // To prevent open resources and instances, only create the services and other references on a run

            // Create scope, so we get request services
            await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();

            // Get service from scope
            var mediator = asyncScope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new GenerateYesterdayOrdersReportCommand());
        }
    }
}
