using Eximia.CsharpCourse.Orders.ProcessPayment;
using Eximia.CsharpCourse.Orders.Repository;
using Eximia.CsharpCourse.SeedWork.EFCore;

namespace Eximia.CsharpCourse.Workers.Jobs;

public class ProcessOrdersPaymentsJob(IServiceScopeFactory factory) : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(10));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && await _timer.WaitForNextTickAsync(stoppingToken))
        {
            // We cannot use the default dependency injection behavior, because ExecuteAsync is
            // a long-running method while the background service is running.
            // To prevent open resources and instances, only create the services and other references on a run

            // Create scope, so we get request services
            await using AsyncServiceScope asyncScope = factory.CreateAsyncScope();
            
            var factory1 = asyncScope.ServiceProvider
                .GetRequiredService<IEFDbContextFactory<EximiaCsharpCourseDbContext>>();
            var accessor = asyncScope.ServiceProvider
                .GetRequiredService<IEFDbContextAccessor<EximiaCsharpCourseDbContext>>();
            var ordersRepository = asyncScope.ServiceProvider.GetRequiredService<IOrdersRepository>();
            
            
            await using var dbContext = factory1.Create();
            accessor.Register(dbContext);

            var orderToProcessPayment = await ordersRepository.GetAllWaitingPayment(stoppingToken);

            foreach (var order in orderToProcessPayment)
            {
                await using AsyncServiceScope asyncScope1 = factory.CreateAsyncScope();
                var processOrderPaymentHandler =
                    asyncScope1.ServiceProvider.GetRequiredService<ProcessOrderPaymentCommandHandler>();
                await processOrderPaymentHandler.Handle(new ProcessOrderPaymentCommand(order.Id), stoppingToken);
            }
            
            await accessor.ClearAsync();
        }
    }
}
