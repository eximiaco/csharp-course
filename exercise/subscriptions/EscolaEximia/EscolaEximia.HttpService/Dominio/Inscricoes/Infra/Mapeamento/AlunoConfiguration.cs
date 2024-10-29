using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Infra.Mapeamento;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Alunos");

        builder.HasKey(a => a.Cpf);

        builder.Property(a => a.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(a => a.Sexo)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<ESexo>())
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(a => a.Idade)
            .IsRequired();
    }
}
