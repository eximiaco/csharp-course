using Eximia.CsharpCourse.Orders.Commands;
using Eximia.CsharpCourse.Payments.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Consumers;

public class RefundOrderPaymentConsumer : IConsumer<PaymentRefundedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public RefundOrderPaymentConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PaymentRefundedIntegrationEvent> context)
    {
        await _mediator.Send(new RefundOrderPaymentCommand(context.Message.OrderId)).ConfigureAwait(false);
    }
}
