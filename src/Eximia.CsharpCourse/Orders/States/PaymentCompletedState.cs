using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class PaymentCompletedState : IOrderState
{
    public string Name => "PaymentCompleted";

    public Result Cancel(Order order)
    {
        if (!order.PaymentMethod.WasRefunded)
            return Result.Failure("Cancelamento não permitido pois o pedido já está pago.");

        order.ChangeState(new CanceledState());
        return Result.Success();
    }

    public Result CompletePayment(Order order)
        => Result.Failure("Pedido já está pago.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está pago.");
}
