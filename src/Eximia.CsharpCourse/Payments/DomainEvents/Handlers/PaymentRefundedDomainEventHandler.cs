using Eximia.CsharpCourse.Orders.Commands;
using MediatR;

namespace Eximia.CsharpCourse.Payments.DomainEvents.Handlers;

public class PaymentRefundedDomainEventHandler : INotificationHandler<PaymentRefundedDomainEvent>
{
    private readonly IMediator _mediator;

    public PaymentRefundedDomainEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(PaymentRefundedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RefundOrderPaymentCommand(notification.Payment.OrderId)).ConfigureAwait(false);
    }
}
