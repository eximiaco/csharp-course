using Eximia.CsharpCourse.Products.IntegrationEvents;
using Eximia.CsharpCourse.Products.Integrations.Stock;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Products.Commands.Handlers;

public class WriteOffProductsFromStockCommandHandler : IRequestHandler<WriteOffProductsFromStockCommand>
{
    private readonly IStockApi _stockApi;
    private readonly IBus _bus;

    public WriteOffProductsFromStockCommandHandler(IStockApi stockApi, IBus bus)
    {
        _stockApi = stockApi;
        _bus = bus;
    }

    public async Task Handle(WriteOffProductsFromStockCommand command, CancellationToken cancellationToken)
    {
        var result = await _stockApi.WriteOffAsync(command.ProductIds, cancellationToken).ConfigureAwait(false);

        // Num cenário real, seria checado quais produtos deram erro e quais não para um tratamento adequado
        if (result.IsFailure)
            await _bus.Publish(new FailureToWriteOffStockIntegrationEvent(command.OrderId), cancellationToken).ConfigureAwait(false);
        else
            await _bus.Publish(new SuccessfullyWrittenOffStockIntegrationEvent(command.OrderId), cancellationToken).ConfigureAwait(false);
    }
}
