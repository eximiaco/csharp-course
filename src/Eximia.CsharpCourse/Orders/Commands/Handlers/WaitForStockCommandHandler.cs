using Eximia.CsharpCourse.Orders.Repository;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class WaitForStockCommandHandler : IRequestHandler<WaitForStockCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public WaitForStockCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(WaitForStockCommand command, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);
        if (order.HasNoValue)
            return;

        var result = order.Value.WaitForStock();
        if (result.IsFailure)
            return;

        await _ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
    }
}
