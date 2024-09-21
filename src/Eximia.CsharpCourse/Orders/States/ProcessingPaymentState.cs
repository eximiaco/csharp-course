using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.DomainEvents;

namespace Eximia.CsharpCourse.Orders.States;

public class ProcessingPaymentState : IOrderState
{
    public string Name => "ProcessingPayment";

    public Result Cancel(Order order)
        => Result.Failure("Pedido está processando pagamento, portanto não pode ser cancelado");

    public Result Complete(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");

    public Result CompletePayment(Order order)
    {
        order.ChangeState(new PaymentCompletedState());
        order.AddDomainEvent(new OrderCompletedDomainEvent(order));
        return Result.Success();
    }

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");

    public Result Separate(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");
}
