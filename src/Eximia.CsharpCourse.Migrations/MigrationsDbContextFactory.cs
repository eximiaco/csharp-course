using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eximia.CsharpCourse.Migrations;

public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<EximiaCsharpCourseContext>
{
    public EximiaCsharpCourseContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings-migrations.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection")!;
        return Create(connectionString);
    }

    private EximiaCsharpCourseContext Create(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));

        var optionsBuilder = new DbContextOptionsBuilder<EximiaCsharpCourseContext>();
        optionsBuilder.UseSqlServer(connectionString, options => {
            options.MigrationsHistoryTable($"{nameof(EximiaCsharpCourseContext)}Migrations");
            options.EnableRetryOnFailure();
            options.CommandTimeout(180);
        });
        return new EximiaCsharpCourseContext(optionsBuilder.Options);
    }
}