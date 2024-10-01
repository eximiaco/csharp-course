using Eximia.CsharpCourse.Orders.Commands;
using Eximia.CsharpCourse.Products.Commands;
using MediatR;

namespace Eximia.CsharpCourse.Orders.DomainEvents.Handlers;

public class OrderIsBeingSeparatedDomainEventHandler : INotificationHandler<OrderIsBeingSeparatedDomainEvent>
{
    private readonly IMediator _mediator;

    public OrderIsBeingSeparatedDomainEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(OrderIsBeingSeparatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new WriteOffProductsFromStockCommand(
            notification.Order.Id,
            notification.Order.Items.Select(i => i.ProductId)),
            cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
            await _mediator.Send(new WaitForStockCommand(notification.Order.Id)).ConfigureAwait(false);
        else
            await _mediator.Send(new CompleteOrderCommand(notification.Order.Id)).ConfigureAwait(false);
    }
}
