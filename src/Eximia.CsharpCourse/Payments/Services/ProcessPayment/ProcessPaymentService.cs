using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments.Integrations.Ebanx;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.Services.ProcessPayment;

public class ProcessPaymentService : IProcessPaymentService
{
    private readonly IEbanxApi _ebanxApi;

    public ProcessPaymentService(IEbanxApi ebanxApi)
    {
        _ebanxApi = ebanxApi;
    }

    public async Task<Result> ProcessAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        Result<string> result;
        if (payment.Method == EPaymentMethod.CreditCard)
            result = await _ebanxApi.CaptureCreditCardTransactionAsync(payment, cancellationToken).ConfigureAwait(false);
        else
            result = await _ebanxApi.CapturePixTransactionAsync(payment, cancellationToken).ConfigureAwait(false);

        if (result.IsSuccess)
            payment.RegisterPayment(result.Value);            
        return result;
    }
}
