using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.DomainEvents;

public record OrderIsAwaitingForStockDomainEvent(Order Order) : IDomainEvent;
