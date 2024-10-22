using EscolaEximia.HttpService.Comum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaEximia.HttpService.Dominio.Regras.infra.Mapeamento;

public class RegraConfiguration: IEntityTypeConfiguration<RegraPorTurma>
{
    public void Configure(EntityTypeBuilder<RegraPorTurma> builder)
    {
        builder.ToTable("RegrasPorTurma");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.TurmaId);
        builder
            .Property(p => p.Regra)
            .HasColumnType("varchar(max)")
            .HasConversion(
                c => c.ToNameTypeJson(),
                s => s.ToNameTypeObject<IValidacaoInscricao>())
            .IsRequired();
    }
}