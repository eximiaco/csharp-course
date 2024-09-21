using Eximia.CsharpCourse.Orders.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.DomainEvents.Handlers;

public class OrderIsAwaitingForStockDomainEventHandler : INotificationHandler<OrderIsAwaitingForStockDomainEvent>
{
    private readonly IBus _bus;

    public OrderIsAwaitingForStockDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderIsAwaitingForStockDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(new OrderIsAwaitingForStockIntegrationEvent(notification.Order.Id), cancellationToken).ConfigureAwait(false);
    }
}
