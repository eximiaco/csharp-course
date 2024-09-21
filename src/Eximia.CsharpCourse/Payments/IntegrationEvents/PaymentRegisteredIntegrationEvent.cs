namespace Eximia.CsharpCourse.Payments.IntegrationEvents;

public record PaymentRegisteredIntegrationEvent(int Id, int OrderId);
