using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class CanceledState : IOrderState
{
    public string Name => "Canceled";

    public Result CanCancel(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result CanComplete(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result CanCompletePayment(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result CanProcessPayment(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result CanSeparate(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido já está cancelado.");
}
