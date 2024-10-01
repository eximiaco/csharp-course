using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments;

namespace Eximia.CsharpCourse.Orders.States;

public class AwaitingProcessingState : IOrderState
{
    public string Name => "AwaitingProcessing";

    public Result Cancel(Order order)
    {
        order.ChangeState(new CanceledState());
        return Result.Success();
    }

    public Result Complete(Order order)
        => Result.Failure("Pedido está aguardando processamento.");

    public Result CompletePayment(Order order, Payment payment)
        => Result.Failure("Pedido está aguardando processamento.");

    public Result ProcessPayment(Order order)
    {
        order.ChangeState(new ProcessingPaymentState());
        return Result.Success();
    }
}
