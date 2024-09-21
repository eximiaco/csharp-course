using Eximia.CsharpCourse.Payments.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Payments.DomainEvents.Handlers;

public class PaymentRegisteredDomainEventHandler : INotificationHandler<PaymentRegisteredDomainEvent>
{
    private readonly IBus _bus;

    public PaymentRegisteredDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(PaymentRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        // Publica um evento de integração indicando que um pagamento foi registrado
        // Os contextos interessados podem se inscrever para receber esse evento
        await _bus.Publish(new PaymentRegisteredIntegrationEvent(notification.Payment.Id, notification.Payment.OrderId), cancellationToken).ConfigureAwait(false);
    }
}
