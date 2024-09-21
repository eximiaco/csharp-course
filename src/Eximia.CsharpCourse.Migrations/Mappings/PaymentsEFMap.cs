using Eximia.CsharpCourse.Migrations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CsharpCourse.Migrations.Mappings;

public class PaymentsEFMap : IEntityTypeConfiguration<Payments>
{
    public void Configure(EntityTypeBuilder<Payments> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);

        builder.Property(o => o.Amount).HasColumnType("numeric(15,2)").IsRequired();
        builder.Property(o => o.ExternalId).HasColumnType("varchar(50)").IsRequired();
        builder.Property(o => o.Method).HasColumnType("varchar(30)").IsRequired();
        builder.Property(o => o.CreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(o => o.Installments).HasColumnType("int").IsRequired(false);
        builder.Property(o => o.WasRefund).HasColumnType("bit").IsRequired();

        builder
            .HasOne(p => p.Order)
            .WithMany()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
