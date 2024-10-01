using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.ProcessPayment;

public record ProcessOrderPaymentCommand(int OrderId) : ICommand;
