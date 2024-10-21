using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Products.Integrations.Stock;
using MediatR;

namespace Eximia.CsharpCourse.Products.Commands.Handlers;

public class WriteOffProductsFromStockCommandHandler : IRequestHandler<WriteOffProductsFromStockCommand, Result>
{
    private readonly IStockApi _stockApi;

    public WriteOffProductsFromStockCommandHandler(IStockApi stockApi)
    {
        _stockApi = stockApi;
    }

    public async Task<Result> Handle(WriteOffProductsFromStockCommand command, CancellationToken cancellationToken)
    {
        // Num cenário real, seria checado quais produtos deram erro e quais não para um tratamento adequado
        return await _stockApi.WriteOffAsync(command.ProductIds, cancellationToken).ConfigureAwait(false);
    }
}
