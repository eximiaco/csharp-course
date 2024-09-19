using Eximia.CsharpCourse.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CsharpCourse.SeedWork.EFCore.Mappings;

public class OrderItemEFMap : IEntityTypeConfiguration<Order.Item>
{
    public void Configure(EntityTypeBuilder<Order.Item> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Amount).HasColumnType("numeric(15,2)").IsRequired();
        builder.Property(o => o.Quantity).HasColumnType("int").IsRequired();
        builder.Property(o => o.ProductId).HasColumnType("int").IsRequired();
    }
}
