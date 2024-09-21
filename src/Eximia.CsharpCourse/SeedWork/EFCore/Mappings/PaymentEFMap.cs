using Eximia.CsharpCourse.Payments;
using Eximia.CsharpCourse.SeedWork.EFCore.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eximia.CsharpCourse.SeedWork.EFCore.Mappings;

public class PaymentEFMap : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount).HasColumnType("numeric(15,2)").IsRequired();
        builder.Property(p => p.OrderId).HasColumnType("int").IsRequired();
        builder.Property(o => o.ExternalId).HasColumnType("varchar(50)").IsRequired();
        builder.Property(o => o.Method).HasConversion(new EnumToStringConverter<EPaymentMethod>()).HasColumnType("varchar(30)").IsRequired();
        builder.Property(o => o.Installments).HasColumnType("int").IsRequired(false);
        builder.Property(o => o.WasRefund).HasColumnType("bit").IsRequired();

        builder
           .Property(o => o.CreatedAt)
           .HasColumnType("datetime")
           .HasConversion(new DateTimeUtcConverter())
           .IsRequired();
    }
}
