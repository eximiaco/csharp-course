using Eximia.CsharpCourse.Products;
using Eximia.CsharpCourse.Products.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CsharpCourse.SeedWork.EFCore.Mappings;

public class ProductEFMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description).HasColumnType("varchar(100)").IsRequired();

        builder
          .Property(p => p.DiscountStrategies)
          .HasColumnType("varchar(max)")
          .HasConversion(
              c => c.ToNameTypeJson(),
              s => s.ToNameTypeObject<IEnumerable<IDiscountStrategy>>())
          .IsRequired();
    }
}
