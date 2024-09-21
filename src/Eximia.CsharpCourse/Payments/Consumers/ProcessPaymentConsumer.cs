using Eximia.CsharpCourse.Orders.IntegrationEvents;
using Eximia.CsharpCourse.Payments.Commands;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Payments.Consumers;

// Utilizando um consumer do MassTransit pois no exemplo estamos utilizando mensageria em memória
// Em um sistema do mundo real, provavelmente seria um outro recurso (como por exemplo, uma Azure Function) que leria esse evento
public class ProcessPaymentConsumer : IConsumer<OrderIsProcessingPaymentIntegrationEvent>
{
    private readonly IMediator _mediator;

    public ProcessPaymentConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderIsProcessingPaymentIntegrationEvent> context)
    {
        await _mediator.Send(new ProcessPaymentCommand(
            context.Message.Id,
            context.Message.Amount,
            context.Message.Method,
            context.Message.Installments)).ConfigureAwait(false);
    }
}
