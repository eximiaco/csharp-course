using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.DomainEvents;

public record PaymentRegisteredDomainEvent(Payment Payment) : IDomainEvent;
