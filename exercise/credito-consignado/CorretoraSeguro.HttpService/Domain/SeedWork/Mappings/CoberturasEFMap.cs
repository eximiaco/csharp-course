using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CorretoraSeguro.HttpService.Domain.SeedWork.Mappings;

public class CoberturasEFMap : IEntityTypeConfiguration<Cobertura>
{
    public void Configure(EntityTypeBuilder<Cobertura> builder)
    {
        builder.ToTable("Coberturas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome).HasColumnType("varchar(100)").IsRequired();
        builder.Property(c => c.Tipo).HasColumnType("varchar(30)").HasConversion(new EnumToStringConverter<ETipoCobertura>()).IsRequired();
        builder.Property(c => c.PercentualCalculo).HasColumnType("numeric(15,2)").IsRequired();
        builder.Property(c => c.TaxaFixa).HasColumnType("bit").IsRequired();
        builder.Property(c => c.ValorFixo).HasColumnType("numeric(15,2)").IsRequired(false);
    }
}
