using Eximia.CsharpCourse.Orders.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.DomainEvents.Handlers;

public class OrderCompletedDomainEventHandler : INotificationHandler<OrderCompletedDomainEvent>
{
    private readonly IBus _bus;

    public OrderCompletedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(new OrderCompletedIntegrationEvent(notification.Order.Id), cancellationToken).ConfigureAwait(false);
    }
}
