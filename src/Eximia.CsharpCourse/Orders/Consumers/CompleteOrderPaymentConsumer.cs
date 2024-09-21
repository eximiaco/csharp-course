using Eximia.CsharpCourse.Orders.Commands;
using Eximia.CsharpCourse.Payments.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Consumers;

// Utilizando um consumer do MassTransit pois no exemplo estamos utilizando mensageria em memória
// Em um sistema do mundo real, provavelmente seria um outro recurso (como por exemplo, uma Azure Function) que leria esse evento
public class CompleteOrderPaymentConsumer : IConsumer<PaymentRegisteredIntegrationEvent>
{
    private readonly IMediator _mediator;

    public CompleteOrderPaymentConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PaymentRegisteredIntegrationEvent> context)
    {
        await _mediator.Send(new CompleteOrderPaymentCommand(context.Message.OrderId)).ConfigureAwait(false);
    }
}
