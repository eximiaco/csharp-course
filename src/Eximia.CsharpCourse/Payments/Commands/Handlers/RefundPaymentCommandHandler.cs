using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments.Repository;
using MediatR;

namespace Eximia.CsharpCourse.Payments.Commands.Handlers;

public class RefundPaymentCommandHandler : IRequestHandler<RefundPaymentCommand, Result>
{
    private readonly IPaymentsRepository _paymentsRepository;

    public RefundPaymentCommandHandler(IPaymentsRepository paymentsRepository)
    {
        _paymentsRepository = paymentsRepository;
    }

    public async Task<Result> Handle(RefundPaymentCommand command, CancellationToken cancellationToken)
    {
        var payment = await _paymentsRepository.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);
        if (payment.HasNoValue)
            return Result.Failure("Pagamento não localizado.");

        payment.Value.Refund();
        await _paymentsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
        return Result.Success();
    }
}
