using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments.Integrations.Ebanx;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.Services.ProcessPayment;

public class ProcessPaymentService(IEbanxApi ebanxApi) : IProcessPaymentService
{
    public async Task<Result> ProcessAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        Result<string> result;
        if (payment.Method == EPaymentMethod.CreditCard)
            result = await ebanxApi.CaptureCreditCardTransactionAsync(payment, cancellationToken).ConfigureAwait(false);
        else
            result = await ebanxApi.CapturePixTransactionAsync(payment, cancellationToken).ConfigureAwait(false);

        if (result.IsSuccess)
            payment.RegisterPayment(result.Value);            
        return result;
    }
}
