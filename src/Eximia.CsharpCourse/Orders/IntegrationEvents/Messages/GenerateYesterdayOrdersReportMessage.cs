namespace Eximia.CsharpCourse.Orders.IntegrationEvents.Messages;

public record GenerateYesterdayOrdersReportMessage(
    int Id,
    string State,
    decimal Amount,
    int QuantityOfItems,
    string PaymentMethod,
    int? Installments,
    bool WasRefunded);
