using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Products.Repository;

public interface IProductsRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByIdsReadOnlyAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
}
