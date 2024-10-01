using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class CompletedState : IOrderState
{
    public string Name => "Completed";

    public Result CanCancel(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result CanComplete(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result CanCompletePayment(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result CanProcessPayment(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result CanSeparate(Order order)
        => Result.Failure("Pedido já está concluído.");

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido já está concluído.");
}
