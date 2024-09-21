using Eximia.CsharpCourse.Orders;
using Eximia.CsharpCourse.Payments;
using Eximia.CsharpCourse.Products;
using Eximia.CsharpCourse.SeedWork.EFCore.Mappings;
using Eximia.CsharpCourse.SeedWork.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eximia.CsharpCourse.SeedWork.EFCore;

public class EximiaCsharpCourseDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public EximiaCsharpCourseDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await _mediator.DispatchDomainEventsAsync(this, cancellationToken).ConfigureAwait(false);
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
