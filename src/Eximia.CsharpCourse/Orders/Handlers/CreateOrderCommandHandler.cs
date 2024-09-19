using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.Commands;
using MediatR;

namespace Eximia.CsharpCourse.Orders.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Order>>
{
    public async Task<Result<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
