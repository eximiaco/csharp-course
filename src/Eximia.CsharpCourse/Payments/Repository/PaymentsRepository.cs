using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.SeedWork.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Eximia.CsharpCourse.Payments.Repository;

public class PaymentsRepository : IPaymentsRepository
{
    private readonly IEFDbContextAccessor<EximiaCsharpCourseDbContext> _dbContextAccessor;

    public PaymentsRepository(IEFDbContextAccessor<EximiaCsharpCourseDbContext> dbContextAccessor)
    {
        _dbContextAccessor = dbContextAccessor;
    }

    public IUnitOfWork UnitOfWork => _dbContextAccessor.Get();

    public async Task AddAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        await _dbContextAccessor
            .Get()
            .Payments
            .AddAsync(payment, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Maybe<Payment>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var payment = await _dbContextAccessor
            .Get()
            .Payments
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            .ConfigureAwait(false);

        if (payment is null)
            return Maybe.None;
        return payment;
    }
}
