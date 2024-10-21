using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class SeparatingOrderState : IOrderState
{
    public string Name => "SeparatingOrder";

    public Result CanCancel(Order order)
    {
        if (!order.PaymentMethod.WasRefunded)
            return Result.Failure("Cancelamento não permitido pois o pedido já está pago.");
        return Result.Success();
    }

    public Result CanComplete(Order order)
        => Result.Success();

    public Result CanCompletePayment(Order order)
        => Result.Failure("Pedido está em separação.");

    public Result CanProcessPayment(Order order)
        => Result.Failure("Pedido está em separação.");

    public Result CanSeparate(Order order)
        => Result.Failure("Pedido já está em separação.");

    public Result WaitForStock(Order order)
        => Result.Success();
}
