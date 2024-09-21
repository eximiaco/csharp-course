using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.Commands;

public record CompleteOrderCommand(int Id) : ICommand;
