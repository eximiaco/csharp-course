using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.Commands;

public record WaitForStockCommand(int Id) : ICommand;
