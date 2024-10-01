using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments;

namespace Eximia.CsharpCourse.Orders.States;

public class PaymentApprovedState : IOrderState
{
    public string Name => "PaymentApprovedCompleted";

    public Result Cancel(Order order)
    {
        if (!order.CanRefund())
            return Result.Failure("Pedido já foi estornado.");
        order.ChangeState(new PaymentApprovedState());
        return Result.Success();
    }

    public Result Complete(Order order)
        => Result.Failure("Pedido já está pago.");

    public Result CompletePayment(Order order, Payment payment)
        => Result.Failure("Pedido já está pago.");

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está pago.");
    
}
