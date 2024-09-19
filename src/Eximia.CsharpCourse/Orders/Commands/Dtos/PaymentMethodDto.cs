using FluentValidation;

namespace Eximia.CsharpCourse.Orders.Commands.Dtos;

public record PaymentMethodDto(EPaymentMethod Method, int? Installments)
{
    public class Validator : AbstractValidator<PaymentMethodDto>
    {
        public Validator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(p => p.Method).IsInEnum().WithMessage("Tipo de pagamento inválido.");
            When(p => p.Method == EPaymentMethod.CreditCard, () =>
            {
                RuleFor(p => p.Installments).GreaterThan(0).WithMessage("O número de parcelas precisa ser informado.");
            });
        }
    }
}
