using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.Repository;

public interface IOrdersRepository : IRepository<Order>
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
}
