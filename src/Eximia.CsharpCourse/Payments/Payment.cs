using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments.DomainEvents;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments;

public class Payment : AggregateRoot<int>
{
    private Payment() { }

    public Payment(
        int id,
        decimal amount,
        DateTime createdAt,
        int orderId,
        int? installments,
        EPaymentMethod method,
        string externalId,
        bool wasRefund,
        EPaymentStatus status) : base(id)
    {
        Amount = amount;
        CreatedAt = createdAt;
        OrderId = orderId;
        Installments = installments;
        Method = method;
        ExternalId = externalId;
        WasRefund = wasRefund;
        Status = status;
    }

    public EPaymentStatus Status { get; private set; }
    public decimal Amount { get; }
    public DateTime CreatedAt { get; }
    public int OrderId { get; }
    public int? Installments { get; }
    public EPaymentMethod Method { get; }
    public string ExternalId { get; private set; } = string.Empty;
    public bool WasRefund { get; private set; }

    public static Payment Create(decimal amount, int orderId, int? installments, EPaymentMethod method)
        => new Payment(id: 0, amount, DateTime.UtcNow, orderId, installments, method, externalId: null!, wasRefund: false, EPaymentStatus.Pending);

    public void RegisterPayment(Result<string> externalId)
    {
        Status = EPaymentStatus.Confirmed;
        if (externalId.IsFailure)
            Status = EPaymentStatus.Denied;

        ExternalId = externalId.Value;
        AddDomainEvent(new PaymentRegisteredDomainEvent(this));
    }

    public void Refund()
    {
        WasRefund = true;
        AddDomainEvent(new PaymentRefundedDomainEvent(this));
    }
}
