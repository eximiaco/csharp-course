using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.DomainEvents;

namespace Eximia.CsharpCourse.Orders.States;

public class ProcessingPaymentState : IOrderState
{
    public string Name => "ProcessingPayment";

    public Result CanCancel(Order order)
        => Result.Failure("Pedido está processando pagamento, portanto não pode ser cancelado");

    public Result CanComplete(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");

    public Result CanCompletePayment(Order order)
        =>  Result.Success();

    public Result CanProcessPayment(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");

    public Result CanSeparate(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");

    public Result WaitForStock(Order order)
        => Result.Failure("Pedido já está processando o pagamento.");
}
