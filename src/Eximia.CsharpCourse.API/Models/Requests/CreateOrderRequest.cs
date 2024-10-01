using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.Commands;
using Eximia.CsharpCourse.Orders.CreateOrder;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.API.Models.Requests;

public record CreateOrderRequest
{
    public IEnumerable<ItemDto> Items { get; set; } = Enumerable.Empty<ItemDto>();
    public PaymentMethodDto PaymentMethod { get; set; } = null!;

    public Result<CreateOrderCommand> CreateCommand()
        => CreateOrderCommand.Create(
            Items?.Select(i => new Orders.Commands.Dtos.ItemDto(i.Amount, i.Quantity, i.ProductId))!,
            PaymentMethod is null
                ? null!
                : new Orders.Commands.Dtos.PaymentMethodDto(PaymentMethod.Method, PaymentMethod.Installments)!);

    public record ItemDto
    {
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
    public record PaymentMethodDto
    {
        public EPaymentMethod Method { get; set; }
        public int? Installments { get; set; }
    }
}
