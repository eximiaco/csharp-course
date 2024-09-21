using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class ProcessingPaymentState : IOrderState
{
    public string Name => "ProcessingPayment";

    public Result Cancel(Order order)
        => Result.Failure("Pedido está processando pagamento, portanto não pode ser cancelado");

    public Result CompletePayment(Order order)
    {
        order.ChangeState(new PaymentCompletedState());
        return Result.Success();
    }

    public Result ProcessPayment(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");
}
