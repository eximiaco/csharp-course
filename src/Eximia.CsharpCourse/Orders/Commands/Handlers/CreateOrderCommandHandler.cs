using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Orders.DomainServices.CalculateOrderValue;
using Eximia.CsharpCourse.Orders.Repository;
using Eximia.CsharpCourse.Products.Integrations.Stock;
using Eximia.CsharpCourse.Products.Repository;
using Eximia.CsharpCourse.SeedWork;
using Eximia.CsharpCourse.SeedWork.EFCore;

namespace Eximia.CsharpCourse.Orders.Commands.Handlers;

public class CreateOrderCommandHandler : IService<CreateOrderCommandHandler>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IStockApi _stockService;
    private readonly ICalculateOrderValueDomainService _calculateOrderValueDomainService;
    private readonly IEfUnitOfWork _efUnitOfWork;

    public CreateOrderCommandHandler(
        IOrdersRepository ordersRepository,
        IProductsRepository productsRepository,
        IStockApi stockService,
        ICalculateOrderValueDomainService calculateOrderValueDomainService,
        IEfUnitOfWork efUnitOfWork)
    {
        _ordersRepository = ordersRepository;
        _productsRepository = productsRepository;
        _stockService = stockService;
        _calculateOrderValueDomainService = calculateOrderValueDomainService;
        _efUnitOfWork = efUnitOfWork;
    }

    public async Task<Result<Order>> CreateOrderAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var productIds = command.Items.Select(i => i.ProductId);
        var products = await _productsRepository.GetByIdsReadOnlyAsync(productIds, cancellationToken).ConfigureAwait(false);

        var productsStock = await _stockService.CheckProductStock(productIds, cancellationToken).ConfigureAwait(false);
        if (productsStock.IsFailure)
            return Result.Failure<Order>(productsStock.Error);
        if (productsStock.Value.Any(p => !p.HasStock))
            return Result.Failure<Order>("Existem produtos sem estoque na sua seleção.");

        var order = Order.Create(
            items: command.Items.Select(i => new Order.Item(i.Amount, i.Quantity, i.ProductId)).ToList(),
            paymentMethod: Order.PaymentMethodInfo.Create(command.PaymentMethod.Method, command.PaymentMethod.Installments));
        if (order.IsFailure)
            return order;

        _calculateOrderValueDomainService.Calculate(order.Value, products);

        await _ordersRepository.AddAsync(order.Value, cancellationToken).ConfigureAwait(false);
        await _efUnitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        return order;
    }
}
