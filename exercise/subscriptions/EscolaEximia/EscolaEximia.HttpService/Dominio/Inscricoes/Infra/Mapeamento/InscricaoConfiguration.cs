using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Infra.Mapeamento;

public class InscricaoConfiguration : IEntityTypeConfiguration<Inscricao>
{
    public void Configure(EntityTypeBuilder<Inscricao> builder)
    {
        builder.ToTable("Inscricoes");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.AlunoCpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(i => i.Responsavel)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Ativa)
            .IsRequired();

        builder.HasOne(i => i.Turma)
            .WithMany()
            .HasForeignKey("TurmaId")
            .IsRequired();

        // Ignorando propriedades que não devem ser mapeadas para colunas
        builder.Ignore(i => i.Turma);
    }
}
