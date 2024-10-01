using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders;

public record OrderCompletedDomainEvent(Order Order) : IDomainEvent;
