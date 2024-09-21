using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.Integrations.Ebanx;

public interface IEbanxApi : IService<IEbanxApi>
{
    Task<Result<string>> CaptureCreditCardTransactionAsync(Payment payment, CancellationToken cancellationToken);
    Task<Result<string>> CapturePixTransactionAsync(Payment payment, CancellationToken cancellationToken);
}
