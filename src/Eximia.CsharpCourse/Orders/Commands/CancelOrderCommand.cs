using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.SeedWork;
using FluentValidation;
using System.Text;

namespace Eximia.CsharpCourse.Orders.Commands;

public record CancelOrderCommand : ICommand<Result>
{
    private CancelOrderCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public static Result<CancelOrderCommand> Create(int id)
    {
        CancelOrderCommand command = new(id);
        var result = new Validator().Validate(command);
        if (result.IsValid)
            return command;

        var errors = result.Errors.Select(validationFailure => validationFailure.ErrorMessage);
        StringBuilder errorMessage = new();
        foreach (string error in errors)
            errorMessage.Append(error).Append(". ");
        return Result.Failure<CancelOrderCommand>(errorMessage.ToString());
    }

    public class Validator : AbstractValidator<CancelOrderCommand>
    {
        public Validator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(c => c.Id).GreaterThan(0).WithMessage("O id do pedido precisa ser informado.");
        }
    }
}
