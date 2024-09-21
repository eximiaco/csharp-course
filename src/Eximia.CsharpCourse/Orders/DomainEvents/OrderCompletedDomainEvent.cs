using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.DomainEvents;

public record OrderCompletedDomainEvent(Order Order) : IDomainEvent;
