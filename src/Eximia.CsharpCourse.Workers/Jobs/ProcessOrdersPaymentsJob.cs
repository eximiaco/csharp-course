using Eximia.CsharpCourse.Orders.ProcessPayment;
using Eximia.CsharpCourse.Orders.Repository;
using Eximia.CsharpCourse.SeedWork.EFCore;

namespace Eximia.CsharpCourse.Workers.Jobs;

public class ProcessOrdersPaymentsJob : BackgroundService
{
    private readonly IServiceScopeFactory _factory;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(10));

    public ProcessOrdersPaymentsJob(IServiceScopeFactory factory)
    {
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && await _timer.WaitForNextTickAsync(stoppingToken))
        {
            // We cannot use the default dependency injection behavior, because ExecuteAsync is
            // a long-running method while the background service is running.
            // To prevent open resources and instances, only create the services and other references on a run

            // Create scope, so we get request services
            await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
            
            var factory = asyncScope.ServiceProvider
                .GetRequiredService<IEFDbContextFactory<EximiaCsharpCourseDbContext>>();
            var accessor = asyncScope.ServiceProvider
                .GetRequiredService<IEFDbContextAccessor<EximiaCsharpCourseDbContext>>();
            var ordersRepository = asyncScope.ServiceProvider.GetRequiredService<IOrdersRepository>();
            var processOrderPaymentHandler =
                asyncScope.ServiceProvider.GetRequiredService<ProcessOrderPaymentCommandHandler>();
            
            await using var dbContext = factory.Create();
            accessor.Register(dbContext);

            var orderToProcessPayment = await ordersRepository.GetAllWaitingPayment(stoppingToken);

            foreach (var order in orderToProcessPayment)
                await processOrderPaymentHandler.Handle(new ProcessOrderPaymentCommand(order.Id), stoppingToken);
            
            await accessor.ClearAsync();
        }
    }
}
