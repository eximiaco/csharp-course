using System.Text;
using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.Commands.Dtos;
using Eximia.CsharpCourse.SeedWork;
using FluentValidation;

namespace Eximia.CsharpCourse.Orders.CreateOrder;

public record CreateOrderCommand : ICommand<Result<Order>>
{
    private CreateOrderCommand(IEnumerable<ItemDto> items, PaymentMethodDto paymentMethod)
    {
        Items = items;
        PaymentMethod = paymentMethod;
    }

    public IEnumerable<ItemDto> Items { get; }
    public PaymentMethodDto PaymentMethod { get; }

    public static Result<CreateOrderCommand> Create(IEnumerable<ItemDto> items, PaymentMethodDto paymentMethod)
    {
        CreateOrderCommand command = new(items, paymentMethod);
        var result = new Validator().Validate(command);
        if (result.IsValid)
            return command;

        var errors = result.Errors.Select(validationFailure => validationFailure.ErrorMessage);
        StringBuilder errorMessage = new();
        foreach (string error in errors)
            errorMessage.Append(error).Append(". ");
        return Result.Failure<CreateOrderCommand>(errorMessage.ToString());
    }

    public class Validator : AbstractValidator<CreateOrderCommand>
    {
        public Validator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(c => c.Items).NotEmpty().WithMessage("Os itens do pedido precisam ser informados.");
            When(c => c.Items?.Any() == true, () =>
            {
                RuleForEach(c => c.Items).SetValidator(new ItemDto.Validator());
            });

            RuleFor(c => c.PaymentMethod).NotNull().WithMessage("A forma de pagamento precisa ser informada.");
            When(c => c.PaymentMethod is not null, () =>
            {
                RuleFor(c => c.PaymentMethod).SetValidator(new PaymentMethodDto.Validator());
            });
        }
    }
}
