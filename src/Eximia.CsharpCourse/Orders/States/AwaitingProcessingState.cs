using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class AwaitingProcessingState : IOrderState
{
    public string Name => "AwaitingProcessing";

    public Result CanCancel(Order order)
        => Result.Success();

    public Result CanComplete(Order order)
        => Result.Failure("Pedido está aguardando processamento.");

    public Result CanCompletePayment(Order order)
        => Result.Failure("Pedido está aguardando processamento.");

    public Result CanProcessPayment(Order order) 
        => Result.Success();

    public Result CanSeparate(Order order)
        => Result.Failure("Pedido está aguardando processamento.");

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido está aguardando processamento.");
}
