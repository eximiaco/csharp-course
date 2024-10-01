using FluentValidation;

namespace Eximia.CsharpCourse.Orders.Commands.Dtos;

public record ItemDto(decimal Amount, int Quantity, int ProductId)
{

    public class Validator : AbstractValidator<ItemDto>
    {
        public Validator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(i => i.Amount).GreaterThan(0).WithMessage("O valor do item precisa ser informado.");
            RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("A quantidade do item precisa ser informada.");
            RuleFor(i => i.ProductId).GreaterThan(0).WithMessage("O produto do item precisa ser informado.");
        }
    }
}
