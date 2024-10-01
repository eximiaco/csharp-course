using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class PaymentCompletedState : IOrderState
{
    public string Name => "PaymentCompleted";

    public Result CanCancel(Order order)
    {
        if (!order.PaymentMethod.WasRefunded)
            return Result.Failure("Cancelamento não permitido pois o pedido já está pago.");
        return Result.Success();
    }

    public Result CanComplete(Order order)
        => Result.Failure("Pedido já está pago.");

    public Result CanCompletePayment(Order order)
        => Result.Failure("Pedido já está pago.");

    public Result CanProcessPayment(Order order)
        => Result.Failure("Pedido já está pago.");

    public Result CanSeparate(Order order)
        => Result.Success();

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido já está pago.");
}
