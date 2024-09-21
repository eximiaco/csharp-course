using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.DomainEvents;

namespace Eximia.CsharpCourse.Orders.States;

public class PaymentCompletedState : IOrderState
{
    public string Name => "PaymentCompleted";

    public Result Cancel(Order order)
    {
        if (!order.PaymentMethod.WasRefunded)
            return Result.Failure("Cancelamento não permitido pois o pedido já está pago.");

        order.ChangeState(new CanceledState());
        order.AddDomainEvent(new OrderCanceledDomainEvent(order));
        return Result.Success();
    }

    public Result Complete(Order order)
        => Result.Failure("Pedido já está pago.");

    public Result CompletePayment(Order order)
        => Result.Failure("Pedido já está pago.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está pago.");

    public Result Separate(Order order)
    {
        order.ChangeState(new SeparatingOrderState());
        order.AddDomainEvent(new OrderIsBeingSeparatedDomainEvent(order));
        return Result.Success();
    }

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido já está pago.");
}
