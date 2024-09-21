using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.States;
using Eximia.CsharpCourse.SeedWork;
using Eximia.CsharpCourse.SeedWork.EFCore;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Order>> GetAllByStateAsync(IOrderState state, CancellationToken cancellationToken)
    {
        return await _dbContextAccessor
            .Get()
            .Orders
            .Include(o => o.Items)
            .TagWithCallSite()
            .Where(o => o.State == state)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Maybe<Order>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _dbContextAccessor
            .Get()
            .Orders
            .Include(o => o.Items)
            .TagWithCallSite()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (order is null)
            return Maybe.None;
        return order;
    }

    public async Task<IEnumerable<Order>> GetAllFromYesterdayReadOnlyAsync(CancellationToken cancellationToken)
    {
        var startDate = DateTime.UtcNow.AddDays(-1).Date;
        var endDate = DateTime.UtcNow.Date.AddMilliseconds(-1);

        return await _dbContextAccessor
            .Get()
            .Orders
            .Include(o => o.Items)
            .TagWithCallSite()
            .AsNoTracking()
            .Where(o => o.Date >= startDate && o.Date <= endDate)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

    }
}
