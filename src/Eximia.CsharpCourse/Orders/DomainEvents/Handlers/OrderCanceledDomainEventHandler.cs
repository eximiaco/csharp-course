using Eximia.CsharpCourse.Orders.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.DomainEvents.Handlers;

public class OrderCanceledDomainEventHandler : INotificationHandler<OrderCanceledDomainEvent>
{
    private readonly IBus _bus;

    public OrderCanceledDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderCanceledDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(new OrderCanceledIntegrationEvent(notification.Order.Id), cancellationToken).ConfigureAwait(false);
    }
}
