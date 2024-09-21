using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class CompletedState : IOrderState
{
    public string Name => "Completed";

    public Result Cancel(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result Complete(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result CompletePayment(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result Separate(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido já está concluído.");
}
