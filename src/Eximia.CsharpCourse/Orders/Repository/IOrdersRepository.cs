using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.Repository;

public interface IOrdersRepository : IRepository<Order>
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task<Maybe<Order>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetAllAwaitingProcessingAsync(CancellationToken cancellationToken);
}
