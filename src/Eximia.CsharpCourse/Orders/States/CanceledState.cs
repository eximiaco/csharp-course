using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class CanceledState : IOrderState
{
    public string Name => "Canceled";

    public Result Cancel(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result Complete(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result CompletePayment(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result Separate(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido já está cancelado.");
}
