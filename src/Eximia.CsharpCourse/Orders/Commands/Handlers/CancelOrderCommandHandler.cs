using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.Repository;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result>
{
    private readonly IOrdersRepository _ordersRepository;

    public CancelOrderCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Result> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);
        if (order.HasNoValue)
            return Result.Failure("Pedido não localizado.");

        var result = order.Value.Cancel();
        if (result.IsFailure)
            return result;

        await _ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}
