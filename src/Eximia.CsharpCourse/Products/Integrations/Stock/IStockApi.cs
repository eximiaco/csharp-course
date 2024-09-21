using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Products.Integrations.Stock.Responses;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Products.Integrations.Stock;

public interface IStockApi : IService<IStockApi>
{
    Task<Result<IEnumerable<StockResponse>>> CheckProductStock(IEnumerable<int> productIds, CancellationToken cancellationToken);
    Task<Result> WriteOffAsync(IEnumerable<int> productIds, CancellationToken cancellationToken);
}
