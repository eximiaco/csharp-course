using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.Repository;
using Eximia.CsharpCourse.Payments;
using Eximia.CsharpCourse.Payments.Services.ProcessPayment;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.ProcessPayment;

public class ProcessOrderPaymentCommandHandler(
    IOrdersRepository ordersRepository,
    IProcessPaymentService paymentService)  : IService<ProcessOrderPaymentCommandHandler>
{
    public async Task<Result> Handle(ProcessOrderPaymentCommand command, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetByIdAsync(command.OrderId, cancellationToken)
            .ConfigureAwait(false);

        if(order.HasNoValue) 
            return Result.Failure("Order is invalid");
        
        order.Value.ProcessPayment();
        var payment = Payment.Create(order.Value.Amount, order.Value.Id, order.Value.PaymentMethod.Installments, 
            order.Value.PaymentMethod.Method);

        var result = await paymentService.ProcessAsync(payment, cancellationToken);
        if(result.IsFailure)
            order.Value.PaymentDenied();
        else
            order.Value.PaymentApproved(payment);
        
        await ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
        
        return Result.Success();
    }
}
