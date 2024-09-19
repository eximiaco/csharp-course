using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.DomainEvents;

public record OrderCreatedDomainEvent(Order Order) : IDomainEvent;
