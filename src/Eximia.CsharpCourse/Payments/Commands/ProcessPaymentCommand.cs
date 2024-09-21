using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.Commands;

public record ProcessPaymentCommand(int Id, decimal Amount, EPaymentMethod Method, int? Installments) : ICommand;
