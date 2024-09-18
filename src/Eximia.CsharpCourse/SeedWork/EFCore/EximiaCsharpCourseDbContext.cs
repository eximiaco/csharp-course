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

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await _mediator.DispatchDomainEventsAsync(this, cancellationToken).ConfigureAwait(false);
        return result > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
