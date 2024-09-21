using Eximia.CsharpCourse.Payments.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Payments.DomainEvents.Handlers;

public class PaymentRefundedDomainEventHandler : INotificationHandler<PaymentRefundedDomainEvent>
{
    private readonly IBus _bus;

    public PaymentRefundedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(PaymentRefundedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(new PaymentRefundedIntegrationEvent(notification.Payment.Id, notification.Payment.OrderId), cancellationToken).ConfigureAwait(false);
    }
}
