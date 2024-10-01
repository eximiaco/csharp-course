using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders;

public record OrderCanceledDomainEvent(Order Order) : IDomainEvent;
