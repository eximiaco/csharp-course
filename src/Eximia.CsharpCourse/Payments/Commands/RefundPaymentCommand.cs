using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Payments.Commands;

public record RefundPaymentCommand(int Id) : ICommand<Result>;
