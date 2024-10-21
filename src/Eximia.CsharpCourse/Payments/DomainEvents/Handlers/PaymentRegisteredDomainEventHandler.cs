using Eximia.CsharpCourse.Orders.Commands;
using MediatR;

namespace Eximia.CsharpCourse.Payments.DomainEvents.Handlers;

public class PaymentRegisteredDomainEventHandler : INotificationHandler<PaymentRegisteredDomainEvent>
{
    private readonly IMediator _mediator;

    public PaymentRegisteredDomainEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(PaymentRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CompleteOrderPaymentCommand(notification.Payment.OrderId)).ConfigureAwait(false);
    }
}
