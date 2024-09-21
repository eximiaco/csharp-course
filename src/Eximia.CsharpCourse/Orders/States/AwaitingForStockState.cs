using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.DomainEvents;

namespace Eximia.CsharpCourse.Orders.States;

public class AwaitingForStockState : IOrderState
{
    public string Name => "AwaitingForStock";

    public Result Cancel(Order order)
    {
        if (!order.PaymentMethod.WasRefunded)
            return Result.Failure("Cancelamento não permitido pois o pedido está aguardando estoque.");

        order.ChangeState(new CanceledState());
        order.AddDomainEvent(new OrderCanceledDomainEvent(order));
        return Result.Success();
    }

    public Result Complete(Order order)
        => Result.Failure("Pedido está aguardando por estoque.");

    public Result CompletePayment(Order order)
        => Result.Failure("Pedido está aguardando por estoque.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido está aguardando por estoque.");

    public Result Separate(Order order)
    {
        // Algum gatilho foi disparado e, após ter estoque, vai para separação novamente
        order.ChangeState(new SeparatingOrderState());
        order.AddDomainEvent(new OrderIsBeingSeparatedDomainEvent(order));
        return Result.Success();
    }

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido está aguardando por estoque.");
}
