using Eximia.CsharpCourse.Payments.Repository;
using Eximia.CsharpCourse.Payments.Services.ProcessPayment;
using MediatR;

namespace Eximia.CsharpCourse.Payments.Commands.Handlers;

public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand>
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IProcessPaymentService _processPaymentService;

    public ProcessPaymentCommandHandler(IPaymentsRepository paymentsRepository, IProcessPaymentService processPaymentService)
    {
        _paymentsRepository = paymentsRepository;
        _processPaymentService = processPaymentService;
    }

    public async Task Handle(ProcessPaymentCommand command, CancellationToken cancellationToken)
    {
        var payment = Payment.Create(command.Amount, command.Id, command.Installments, command.Method);
        var result = await _processPaymentService.ProcessAsync(payment);
        if (result.IsFailure)
            return;

        await _paymentsRepository.AddAsync(payment).ConfigureAwait(false);
        await _paymentsRepository.UnitOfWork.SaveEntitiesAsync().ConfigureAwait(false);
    }
}
