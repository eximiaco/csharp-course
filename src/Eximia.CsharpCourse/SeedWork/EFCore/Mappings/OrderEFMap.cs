using Eximia.CsharpCourse.Orders;
using Eximia.CsharpCourse.SeedWork.EFCore.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eximia.CsharpCourse.SeedWork.EFCore.Mappings;

public class OrderEFMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Date)
            .HasColumnType("datetime")
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired();

        builder.OwnsOne(o => o.PaymentMethod, paymentMethod =>
        {
            paymentMethod
                .Property(p => p.Method)
                .HasColumnName("PaymentMethod")
                .HasConversion(new EnumToStringConverter<EPaymentMethod>())
                .HasColumnType("varchar(20)")
                .IsRequired();

            paymentMethod.Property(p => p.Installments).HasColumnName("PaymentMethodInstallments").HasColumnType("int").IsRequired(false);
        });

        builder
            .Property(p => p.State)
            .HasColumnType("varchar(50)")
            .HasColumnName("Status")
            .HasConversion(new OrderStatusConverter())
            .IsRequired();

        builder
            !.HasMany(p => p.Items)
            !.WithOne()
            !.HasForeignKey("OrderId")
            !.OnDelete(DeleteBehavior.Cascade)
            !.IsRequired()
            !.Metadata
            !.PrincipalToDependent
            !.SetField("_items");
    }
}
