using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.Repository;

public interface IPaymentsRepository : IRepository<Payment>
{
    Task AddAsync(Payment payment, CancellationToken cancellationToken = default);
    Task<Maybe<Payment>> GetByIdAsync(int id, CancellationToken cancellationToken);
}
