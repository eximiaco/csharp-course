using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eximia.CsharpCourse.SeedWork.EFCore;

public class EximiaCsharpCourseDbContextFactory(IConfiguration configuration, IBus bus)
    : IEFDbContextFactory<EximiaCsharpCourseDbContext>
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    public EximiaCsharpCourseDbContext Create()
    {
        var options = new DbContextOptionsBuilder<EximiaCsharpCourseDbContext>()
            .UseSqlServer(_connectionString, options => options.EnableRetryOnFailure())
            .Options;

        return new EximiaCsharpCourseDbContext(options, bus);
    }
}
