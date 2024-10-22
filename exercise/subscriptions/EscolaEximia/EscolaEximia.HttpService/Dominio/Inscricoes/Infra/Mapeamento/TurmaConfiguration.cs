using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Infra.Mapeamento;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("Turmas");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Vagas)
            .IsRequired();

        builder.Property(t => t.Masculino)
            .IsRequired();

        builder.Property(t => t.Feminino)
            .IsRequired();

        builder.Property(t => t.LimiteIdade)
            .IsRequired();
    }
}
