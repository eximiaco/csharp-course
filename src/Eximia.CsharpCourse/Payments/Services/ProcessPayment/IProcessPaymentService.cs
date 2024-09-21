using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.Services.ProcessPayment;

public interface IProcessPaymentService : IService<IProcessPaymentService>
{
    Task<Result> ProcessAsync(Payment payment, CancellationToken cancellationToken = default);
}
