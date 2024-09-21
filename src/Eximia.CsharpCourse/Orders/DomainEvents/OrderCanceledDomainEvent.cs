using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.DomainEvents;

public record OrderCanceledDomainEvent(Order Order) : IDomainEvent;
