using Eximia.CsharpCourse.Orders.Repository;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class GenerateYesterdayOrdersReportCommandHandler : IRequestHandler<GenerateYesterdayOrdersReportCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public GenerateYesterdayOrdersReportCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(GenerateYesterdayOrdersReportCommand request, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetAllFromYesterdayReadOnlyAsync(cancellationToken).ConfigureAwait(false);
        // Publica mensagens com o pedido recuperado para que o contexto correto gere o relatório
    }
}
