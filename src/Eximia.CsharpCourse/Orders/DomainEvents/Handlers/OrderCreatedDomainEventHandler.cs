using Eximia.CsharpCourse.Orders.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.DomainEvents.Handlers;

public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IBus _bus;

    public OrderCreatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // Publica um evento de integração indicando que um pedido foi criado
        // Os contextos interessados podem se inscrever para receber esse evento (como o controle de estoque, por exemplo)
        await _bus.Publish(new OrderCreatedIntegrationEvent(notification.Order.Id), cancellationToken).ConfigureAwait(false);
    }
}
