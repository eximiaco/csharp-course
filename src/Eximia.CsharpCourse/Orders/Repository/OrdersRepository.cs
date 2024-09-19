using Eximia.CsharpCourse.SeedWork;
using Eximia.CsharpCourse.SeedWork.EFCore;

namespace Eximia.CsharpCourse.Orders.Repository;

public class OrdersRepository : IOrdersRepository
{
    private readonly IEFDbContextAccessor<EximiaCsharpCourseDbContext> _dbContextAccessor;

    public OrdersRepository(IEFDbContextAccessor<EximiaCsharpCourseDbContext> dbContextAccessor)
    {
        _dbContextAccessor = dbContextAccessor;
    }

    public IUnitOfWork UnitOfWork => _dbContextAccessor.Get();

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await _dbContextAccessor
            .Get()
            .Orders
            .AddAsync(order, cancellationToken)
            .ConfigureAwait(false);
    }
}
