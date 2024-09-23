namespace Eximia.CsharpCourse.UnitTests;

using Bogus;
using Eximia.CsharpCourse.Orders;
using Eximia.CsharpCourse.Orders.DomainEvents;
using FluentAssertions;
using static Eximia.CsharpCourse.Orders.Order;

public class OrderTests
{
    [Fact]
    public void ShouldNotAllowMoreThanTwelveInstallmentsIfPaymentIsCreditCard()
    {
        var faker = new Faker();
        var paymentInfo = new PaymentMethodInfo(
            method: SeedWork.EPaymentMethod.CreditCard,
            installments: faker.Random.Number(13, 100),
            wasRefunded: true
        );

        var orderResult = Order.Create(Array.Empty<Item>(), paymentInfo);

        orderResult.IsSuccess.Should().BeFalse();
        orderResult.Error.Should().NotBeNull();
    }

    [Fact]
    public void ShouldCancelOrder()
    {
        var paymentInfo = new PaymentMethodInfo(
            method: SeedWork.EPaymentMethod.Pix,
            installments: null,
            wasRefunded: true
        );
        var orderResult = Order.Create(Array.Empty<Item>(), paymentInfo);
        var order = orderResult.Value;

        order.Cancel();
        order.State.Name.Should().Be("Canceled");
    }

    [Fact]
    public void ShouldCreateOrder()
    {
        var faker = new Faker();
        var paymentInfo = new PaymentMethodInfo(
            method: SeedWork.EPaymentMethod.Pix, 
            installments: null, 
            wasRefunded: true
        );
        var items = Enumerable.Range(1, 3).Select(_ =>
        {
            return new Item(
                amount: new Faker().Random.Decimal(1, 3),
                quantity: faker.Random.Number(1, 10),
                productId: faker.Random.Number(1, 10)
            );
        }).ToList();

        var orderResult = Order.Create(items, paymentInfo);

        var createdOrder = orderResult.Value;
        orderResult.IsSuccess.Should().BeTrue();
        createdOrder.Items.Should().BeEquivalentTo(items);
        createdOrder.DomainEvents.Should().NotBeEmpty();
        createdOrder.DomainEvents.First().Should().BeOfType<OrderCreatedDomainEvent>();
        createdOrder.PaymentMethod.Should().BeEquivalentTo(paymentInfo);
    }
}