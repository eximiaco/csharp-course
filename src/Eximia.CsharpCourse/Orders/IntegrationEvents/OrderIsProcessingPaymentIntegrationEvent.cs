using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.IntegrationEvents;

public record OrderIsProcessingPaymentIntegrationEvent(int Id, decimal Amount, EPaymentMethod Method, int? Installments);
