using Eximia.CsharpCourse.Orders.Repository;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class RefundOrderPaymentCommandHandler : IRequestHandler<RefundOrderPaymentCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public RefundOrderPaymentCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(RefundOrderPaymentCommand command, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);
        if (order.HasNoValue)
            return;

        order.Value.Refund();
        await _ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
    }
}
