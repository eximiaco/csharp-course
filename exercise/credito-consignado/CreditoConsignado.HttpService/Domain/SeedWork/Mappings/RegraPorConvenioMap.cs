using CreditoConsignado.HttpService.Domain.Convenios;
using CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditoConsignado.HttpService.Domain.SeedWork.Mappings;
using System.Text.Json;

public sealed class RegraPorConvenioMap: IEntityTypeConfiguration<RegraPorConvenio>
{
    public void Configure(EntityTypeBuilder<RegraPorConvenio> builder)
    {
        builder.ToTable("RegrasCriacaoProposta");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ConvenioId)
            .HasColumnType("varchar(36)")
            .IsRequired();

        builder.Property(r => r.Nome)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(r => r.Regra)
            .HasColumnType("nvarchar(max)")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
                v => JsonSerializer.Deserialize<IRegraCriacaoProposta>(v, new JsonSerializerOptions { WriteIndented = true })!
            )
            .IsRequired();

        builder.Property(r => r.Ativa)
            .HasColumnType("bit")
            .IsRequired();

        builder.HasOne<Convenio>()
            .WithMany()
            .HasForeignKey(r => r.ConvenioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}