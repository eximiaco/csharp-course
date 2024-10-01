using Eximia.CsharpCourse.Payments.Commands;
using MediatR;

namespace Eximia.CsharpCourse.Orders.DomainEvents.Handlers;

public class OrderIsProcessingPaymentDomainEventHandler : INotificationHandler<OrderIsProcessingPaymentDomainEvent>
{
    private readonly IMediator _mediator;

    public OrderIsProcessingPaymentDomainEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(OrderIsProcessingPaymentDomainEvent notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ProcessPaymentCommand(
            notification.Order.Id,
            notification.Order.Amount,
            notification.Order.PaymentMethod.Method,
            notification.Order.PaymentMethod.Installments)).ConfigureAwait(false);
    }
}
