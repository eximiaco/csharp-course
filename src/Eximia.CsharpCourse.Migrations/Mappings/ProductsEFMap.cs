using Eximia.CsharpCourse.Migrations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CsharpCourse.Migrations.Mappings;

public class ProductsEFMap : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description).HasColumnType("varchar(100)").IsRequired();
        builder.Property(p => p.DiscountStrategies).HasColumnType("varchar(max)").IsRequired();
    }
}