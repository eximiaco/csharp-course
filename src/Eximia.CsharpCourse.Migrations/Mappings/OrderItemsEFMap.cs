using Eximia.CsharpCourse.Migrations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CsharpCourse.Migrations.Mappings;

public class OrderItemsEFMap : IEntityTypeConfiguration<OrderItems>
{
    public void Configure(EntityTypeBuilder<OrderItems> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Amount).HasColumnType("numeric(15,2)").IsRequired();
        builder.Property(o => o.Quantity).HasColumnType("int").IsRequired();

        builder
            .HasOne(o => o.Order)
            .WithMany()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
