using Eximia.CsharpCourse.SeedWork.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Eximia.CsharpCourse.Products.Repository;

public class ProductsRepository : IProductsRepository
{
    private readonly IEFDbContextAccessor<EximiaCsharpCourseDbContext> _dbContextAccessor;

    public ProductsRepository(IEFDbContextAccessor<EximiaCsharpCourseDbContext> dbContextAccessor)
    {
        _dbContextAccessor = dbContextAccessor;
    }

    public IUnitOfWork UnitOfWork => _dbContextAccessor.Get();

    public async Task<IEnumerable<Product>> GetByIdsReadOnlyAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        return await _dbContextAccessor
            .Get()
            .Products
            .TagWithCallSite()
            .AsNoTracking()
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
