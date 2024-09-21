using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.DomainEvents;

public record OrderIsBeingSeparatedDomainEvent(Order Order) : IDomainEvent;
