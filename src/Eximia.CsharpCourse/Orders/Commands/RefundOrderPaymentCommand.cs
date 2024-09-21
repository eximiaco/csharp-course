using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.Commands;

public record RefundOrderPaymentCommand(int Id) : ICommand;
