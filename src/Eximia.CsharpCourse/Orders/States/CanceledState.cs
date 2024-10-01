using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments;

namespace Eximia.CsharpCourse.Orders.States;

public class CanceledState : IOrderState
{
    public string Name => "Canceled";

    public Result Cancel(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result Complete(Order order)
        => Result.Failure("Pedido já está cancelado.");

    public Result CompletePayment(Order order, Payment payment)
        => Result.Failure("Pedido já está cancelado.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está cancelado.");
}
