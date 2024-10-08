﻿using Eximia.CsharpCourse.Orders.Repository;
using Eximia.CsharpCourse.Orders.States;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class ProcessOrdersPaymentsCommandHandler : IRequestHandler<ProcessOrdersPaymentsCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public ProcessOrdersPaymentsCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(ProcessOrdersPaymentsCommand command, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetAllByStateAsync(new AwaitingProcessingState(), cancellationToken).ConfigureAwait(false);
        foreach (var order in orders)
            order.ProcessPayment();

        await _ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
    }
}
