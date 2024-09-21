using Eximia.CsharpCourse.Orders.IntegrationEvents;
using Eximia.CsharpCourse.Products.Commands;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Products.Consumers;

// Utilizando um consumer do MassTransit pois no exemplo estamos utilizando mensageria em memória
// Em um sistema do mundo real, provavelmente seria um outro recurso (como por exemplo, uma Azure Function) que leria esse evento
public class WriteOffProductsFromStockConsumer : IConsumer<OrderIsBeingSeparatedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public WriteOffProductsFromStockConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderIsBeingSeparatedIntegrationEvent> context)
    {
        await _mediator.Send(new WriteOffProductsFromStockCommand(context.Message.Id, context.Message.ProductIds)).ConfigureAwait(false);
    }
}
