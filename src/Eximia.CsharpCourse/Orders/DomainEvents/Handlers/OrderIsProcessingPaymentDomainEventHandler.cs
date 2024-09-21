using Eximia.CsharpCourse.Orders.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.DomainEvents.Handlers;

public class OrderIsProcessingPaymentDomainEventHandler : INotificationHandler<OrderIsProcessingPaymentDomainEvent>
{
    private readonly IBus _bus;

    public OrderIsProcessingPaymentDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderIsProcessingPaymentDomainEvent notification, CancellationToken cancellationToken)
    {
        // Publica um evento de integração indicando que um pedido está processando o pagamento
        // Os contextos interessados podem se inscrever para receber esse evento
        await _bus.Publish(new OrderIsProcessingPaymentIntegrationEvent(
            notification.Order.Id,
            notification.Order.Amount,
            notification.Order.PaymentMethod.Method,
            notification.Order.PaymentMethod.Installments), cancellationToken).ConfigureAwait(false);
    }
}
