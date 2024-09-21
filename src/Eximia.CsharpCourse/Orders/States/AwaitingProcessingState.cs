using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.DomainEvents;

namespace Eximia.CsharpCourse.Orders.States;

public class AwaitingProcessingState : IOrderState
{
    public string Name => "AwaitingProcessing";

    public Result Cancel(Order order)
    {
        order.ChangeState(new CanceledState());
        return Result.Success();
    }

    public Result CompletePayment(Order order)
        => Result.Failure("Pedido está aguardando processamento.");

    public Result ProcessPayment(Order order)
    {
        order.ChangeState(new ProcessingPaymentState());
        order.AddDomainEvent(new OrderIsProcessingPaymentDomainEvent(order));
        return Result.Success();
    }
}
