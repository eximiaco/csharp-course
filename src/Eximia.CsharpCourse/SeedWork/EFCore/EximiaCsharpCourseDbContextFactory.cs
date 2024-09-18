using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eximia.CsharpCourse.SeedWork.EFCore;

public class EximiaCsharpCourseDbContextFactory : IEFDbContextFactory<EximiaCsharpCourseDbContext>
{
    private readonly string _connectionString;
    private readonly IMediator _mediator;

    public EximiaCsharpCourseDbContextFactory(IConfiguration configuration, IMediator mediator)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        _mediator = mediator;
    }

    public EximiaCsharpCourseDbContext Create()
    {
        var options = new DbContextOptionsBuilder<EximiaCsharpCourseDbContext>()
            .UseSqlServer(_connectionString, options => options.EnableRetryOnFailure())
            .Options;

        return new EximiaCsharpCourseDbContext(options, _mediator);
    }
}
