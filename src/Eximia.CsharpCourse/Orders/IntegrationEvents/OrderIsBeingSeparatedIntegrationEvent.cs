namespace Eximia.CsharpCourse.Orders.IntegrationEvents;

public record OrderIsBeingSeparatedIntegrationEvent(int Id, IEnumerable<int> ProductIds);
