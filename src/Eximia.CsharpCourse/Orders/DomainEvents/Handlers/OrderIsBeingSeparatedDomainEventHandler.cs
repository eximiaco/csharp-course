using Eximia.CsharpCourse.Orders.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.DomainEvents.Handlers;

public class OrderIsBeingSeparatedDomainEventHandler : INotificationHandler<OrderIsBeingSeparatedDomainEvent>
{
    private readonly IBus _bus;

    public OrderIsBeingSeparatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderIsBeingSeparatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // Publica um evento de integração indicando que um pedido está sendo separado
        // Os contextos interessados podem se inscrever para receber esse evento
        await _bus.Publish(new OrderIsBeingSeparatedIntegrationEvent(
            notification.Order.Id,
            notification.Order.Items.Select(i => i.ProductId)), cancellationToken).ConfigureAwait(false);
    }
}
