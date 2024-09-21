using Eximia.CsharpCourse.Migrations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CsharpCourse.Migrations.Mappings;

public class OrdersEFMap : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Status).HasColumnType("varchar(50)").IsRequired();
        builder.Property(o => o.PaymentMethod).HasColumnType("varchar(20)").IsRequired();
        builder.Property(o => o.PaymentMethodInstallments).HasColumnType("int").IsRequired(false);
        builder.Property(o => o.PaymentMethodWasRefunded).HasColumnType("bit").IsRequired();
        builder.Property(o => o.Date).HasColumnType("datetime").IsRequired();
    }
}
