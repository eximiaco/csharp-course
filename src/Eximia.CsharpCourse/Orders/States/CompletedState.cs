using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments;

namespace Eximia.CsharpCourse.Orders.States;

public class CompletedState : IOrderState
{
    public string Name => "Completed";

    public Result Cancel(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result Complete(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result CompletePayment(Order order, Payment payment)
        => Result.Failure("Pedido já está concluído.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está concluído.");

}
