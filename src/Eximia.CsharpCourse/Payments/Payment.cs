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
        bool wasRefund) : base(id)
    {
        Amount = amount;
        CreatedAt = createdAt;
        OrderId = orderId;
        Installments = installments;
        Method = method;
        ExternalId = externalId;
        WasRefund = wasRefund;
    }

    public decimal Amount { get; }
    public DateTime CreatedAt { get; }
    public int OrderId { get; }
    public int? Installments { get; }
    public EPaymentMethod Method { get; }
    public string ExternalId { get; private set; } = string.Empty;
    public bool WasRefund { get; private set; }

    public static Payment Create(decimal amount, int orderId, int? installments, EPaymentMethod method)
        => new Payment(id: 0, amount, DateTime.UtcNow, orderId, installments, method, externalId: null!, wasRefund: false);

    public void RegisterPayment(string externalId)
    {
        ExternalId = externalId;
        AddDomainEvent(new PaymentRegisteredDomainEvent(this));
    }

    public void Refund()
    {
        WasRefund = true;
        AddDomainEvent(new PaymentRefundedDomainEvent(this));
    }
}
