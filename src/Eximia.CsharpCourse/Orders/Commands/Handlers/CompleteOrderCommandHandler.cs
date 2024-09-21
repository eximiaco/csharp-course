using Eximia.CsharpCourse.Orders.Repository;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public CompleteOrderCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(CompleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);
        if (order.HasNoValue)
            return;

        var result = order.Value.Complete();
        if (result.IsFailure)
            return;

        await _ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
    }
}
