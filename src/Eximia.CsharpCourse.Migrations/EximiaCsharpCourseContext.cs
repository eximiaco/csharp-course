using Eximia.CsharpCourse.Migrations.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Eximia.CsharpCourse.Migrations;

public class EximiaCsharpCourseContext : DbContext
{
    public EximiaCsharpCourseContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ReplaceService<IRelationalCommandBuilderFactory, DynamicSqlRelationalCommandBuilderFactory>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductsEFMap());
        modelBuilder.ApplyConfiguration(new OrdersEFMap());
        modelBuilder.ApplyConfiguration(new OrderItemsEFMap());
    }
}
