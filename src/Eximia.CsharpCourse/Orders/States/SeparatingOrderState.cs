using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.DomainEvents;

namespace Eximia.CsharpCourse.Orders.States;

public class SeparatingOrderState : IOrderState
{
    public string Name => "SeparatingOrder";

    public Result Cancel(Order order)
    {
        if (!order.PaymentMethod.WasRefunded)
            return Result.Failure("Cancelamento não permitido pois o pedido já está pago.");

        order.ChangeState(new CanceledState());
        order.AddDomainEvent(new OrderCanceledDomainEvent(order));
        return Result.Success();
    }

    public Result Complete(Order order)
    {
        order.ChangeState(new CompletedState());
        return Result.Success();
    }

    public Result CompletePayment(Order order)
        => Result.Failure("Pedido está em separação.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido está em separação.");

    public Result Separate(Order order)
        => Result.Failure("Pedido já está em separação.");

    public Result WaitForStock(Order order)
    {
        order.ChangeState(new AwaitingForStockState());
        order.AddDomainEvent(new OrderIsAwaitingForStockDomainEvent(order));
        return Result.Success();
    }
}
