﻿using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.States;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.Repository;

public interface IOrdersRepository : IRepository<Order>
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task<Maybe<Order>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetAllByStateAsync(IOrderState state, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetAllFromYesterdayReadOnlyAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetAllWaitingPayment(CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsById(IEnumerable<int> ids, CancellationToken cancellationToken);
}
