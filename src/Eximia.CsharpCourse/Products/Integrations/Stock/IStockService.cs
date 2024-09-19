using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Products.Integrations.Stock.Responses;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Products.Integrations.Stock;

public interface IStockService : IService<IStockService>
{
    Task<Result<IEnumerable<StockResponse>>> CheckProductStock(IEnumerable<int> productIds, CancellationToken cancellationToken);
}
