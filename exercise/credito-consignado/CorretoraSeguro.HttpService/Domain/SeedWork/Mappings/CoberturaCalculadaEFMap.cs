using CorretoraSeguro.HttpService.Domain.Cotacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CorretoraSeguro.HttpService.Domain.SeedWork.Mappings;

public class CoberturaCalculadaEFMap : IEntityTypeConfiguration<CoberturaCalculada>
{
    public void Configure(EntityTypeBuilder<CoberturaCalculada> builder)
    {
        builder.ToTable("CotacaoCoberturasCalculadas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Tipo).HasColumnType("varchar(30)").HasConversion(new EnumToStringConverter<ETipoCobertura>()).IsRequired();
        builder.Property(c => c.Valor).HasColumnType("numeric(15,2)").IsRequired();
    }
}
