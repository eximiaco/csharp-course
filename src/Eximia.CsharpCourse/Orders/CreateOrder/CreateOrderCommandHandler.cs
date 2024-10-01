using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.DomainServices.CalculateOrderValue;
using Eximia.CsharpCourse.Orders.Repository;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.CreateOrder;

public class CreateOrderCommandHandler(
    IOrdersRepository ordersRepository,
    ICalculateOrderValueDomainService calculateOrderValueDomainService)
    : IService<CreateOrderCommandHandler>
{
    public async Task<Result<Order>> Execute(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var productIds = command.Items.Select(i => i.ProductId);
        var products = await ordersRepository.GetProductsById(productIds, cancellationToken).ConfigureAwait(false);
        
        var order = Order.Create(
            items: command.Items.Select(i => new Order.Item(i.Amount, i.Quantity, i.ProductId)).ToList(),
            paymentMethod: Order.PaymentMethodInfo.Create(command.PaymentMethod.Method, command.PaymentMethod.Installments));
        if (order.IsFailure)
            return order;

        calculateOrderValueDomainService.Calculate(order.Value, products);

        await ordersRepository.AddAsync(order.Value, cancellationToken).ConfigureAwait(false);
        await ordersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
        return order;
    }
}
