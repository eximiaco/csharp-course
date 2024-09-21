using Eximia.CsharpCourse.Orders.IntegrationEvents.Messages;
using Eximia.CsharpCourse.Orders.Repository;
using MassTransit;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class GenerateYesterdayOrdersReportCommandHandler : IRequestHandler<GenerateYesterdayOrdersReportCommand>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IBus _bus;

    public GenerateYesterdayOrdersReportCommandHandler(IOrdersRepository ordersRepository, IBus bus)
    {
        _ordersRepository = ordersRepository;
        _bus = bus;
    }

    public async Task Handle(GenerateYesterdayOrdersReportCommand request, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetAllFromYesterdayReadOnlyAsync(cancellationToken).ConfigureAwait(false);
        foreach (var order in orders)
            await _bus.Send(new GenerateYesterdayOrdersReportMessage(
                order.Id,
                order.State.Name,
                order.Amount,
                order.Items.Count(),
                order.PaymentMethod.Method.ToString(),
                order.PaymentMethod.Installments,
                order.PaymentMethod.WasRefunded));
    }
}
