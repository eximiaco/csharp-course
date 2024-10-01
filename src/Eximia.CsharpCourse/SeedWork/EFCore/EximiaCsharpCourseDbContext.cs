using Eximia.CsharpCourse.Orders;
using Eximia.CsharpCourse.Payments;
using Eximia.CsharpCourse.SeedWork.EFCore.Mappings;
using Eximia.CsharpCourse.SeedWork.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eximia.CsharpCourse.SeedWork.EFCore;

public class EximiaCsharpCourseDbContext(DbContextOptions options, IBus bus) 
    : DbContext(options), IUnitOfWork
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await bus.DispatchDomainEventsAsync(this, cancellationToken).ConfigureAwait(false);
        return result > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEFMap());
        modelBuilder.ApplyConfiguration(new OrderEFMap());
        modelBuilder.ApplyConfiguration(new OrderItemEFMap());
        modelBuilder.ApplyConfiguration(new PaymentEFMap());
    }
}
