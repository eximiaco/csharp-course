using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class AwaitingForStockState : IOrderState
{
    public string Name => "AwaitingForStock";

    public Result CanCancel(Order order)
    {
        if (!order.PaymentMethod.WasRefunded)
            return Result.Failure("Cancelamento não permitido pois o pedido está aguardando estoque.");
        return Result.Success();
    }

    public Result CanComplete(Order order)
        => Result.Failure("Pedido está aguardando por estoque.");

    public Result CanCompletePayment(Order order)
        => Result.Failure("Pedido está aguardando por estoque.");

    public Result CanProcessPayment(Order order)
        => Result.Failure("Pedido está aguardando por estoque.");

    public Result CanSeparate(Order order)
    {
        // Algum gatilho foi disparado e, após ter estoque, vai para separação novamente
        return Result.Success();
    }

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido está aguardando por estoque.");
}
