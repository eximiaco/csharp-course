using Eximia.CsharpCourse.Orders.Repository;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class CompleteOrderPaymentCommandHandler : IRequestHandler<CompleteOrderPaymentCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public CompleteOrderPaymentCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(CompleteOrderPaymentCommand command, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);
        if (order.HasNoValue)
            return;

        var result = order.Value.CompletePayment();
        if (result.IsFailure)
            return;

        await _ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
    }
}
