using Eximia.CsharpCourse.Orders.Commands;
using Eximia.CsharpCourse.Products.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Consumers;

// Utilizando um consumer do MassTransit pois no exemplo estamos utilizando mensageria em memória
// Em um sistema do mundo real, provavelmente seria um outro recurso (como por exemplo, uma Azure Function) que leria esse evento
public class WaitForStockConsumer : IConsumer<FailureToWriteOffStockIntegrationEvent>
{
    private readonly IMediator _mediator;

    public WaitForStockConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<FailureToWriteOffStockIntegrationEvent> context)
    {
        await _mediator.Send(new WaitForStockCommand(context.Message.OrderId)).ConfigureAwait(false);
    }
}
