using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.Commands;

public record CompleteOrderPaymentCommand(int Id) : ICommand;
