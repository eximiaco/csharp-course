using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.CreateOrder;

public record OrderCreatedDomainEvent(Order Order) : IDomainEvent;
