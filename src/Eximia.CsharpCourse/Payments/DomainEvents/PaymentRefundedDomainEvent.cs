using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.DomainEvents;

public record PaymentRefundedDomainEvent(Payment Payment) : IDomainEvent;
