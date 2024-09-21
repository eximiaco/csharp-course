using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.DomainEvents;

public record OrderIsProcessingPaymentDomainEvent(Order Order) : IDomainEvent;
