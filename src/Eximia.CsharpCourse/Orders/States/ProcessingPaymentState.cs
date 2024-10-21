using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments;

namespace Eximia.CsharpCourse.Orders.States;

public class ProcessingPaymentState : IOrderState
{
    public string Name => "ProcessingPayment";

    public Result Cancel(Order order)
    {
        order.ChangeState(new CanceledState());
        return Result.Success();
    }

    public Result Complete(Order order)
        => Result.Failure("Pedido está tendo seu pagamento processado.");

    public Result CompletePayment(Order order, Payment payment)
    {
        if (payment.Status == EPaymentStatus.Confirmed)
           order.ChangeState(new CompletedState());
        else    
            order.ChangeState(new CanceledState());
        return Result.Success();
    }

    public Result ProcessPayment(Order order)
        => Result.Success();
}
