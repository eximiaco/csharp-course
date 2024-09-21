using Eximia.CsharpCourse.Orders.Repository;
using Eximia.CsharpCourse.Orders.States;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class SeparateOrdersCommandHandler : IRequestHandler<SeparateOrdersCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public SeparateOrdersCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(SeparateOrdersCommand command, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetAllByStateAsync(new PaymentCompletedState(), cancellationToken).ConfigureAwait(false);
        foreach (var order in orders)
            order.Separate();

        await _ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
    }
}
