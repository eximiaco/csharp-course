using CorretoraSeguro.HttpService.Domain.Cotacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CorretoraSeguro.HttpService.Domain.SeedWork.Mappings;

public class CotacoesEFMap : IEntityTypeConfiguration<Cotacao>
{
    public void Configure(EntityTypeBuilder<Cotacao> builder)
    {
        builder.ToTable("Cotacoes");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Status).HasColumnType("varchar(30)").HasConversion(new EnumToStringConverter<EStatusCotacao>()).IsRequired();
        builder.Property(c => c.NivelRisco).HasColumnType("int").IsRequired(false);
        builder.Property(c => c.ValorBase).HasColumnType("numeric(15,2)").IsRequired(false);
        builder.Property(c => c.ValorFinal).HasColumnType("numeric(15,2)").IsRequired(false);
        builder.Property(c => c.DataCriacao).HasColumnType("datetime").IsRequired();
        builder.Property(c => c.DataAprovacao).HasColumnType("datetime").IsRequired(false);

        // Exemplo de Value Object quebrando um campo por coluna
        builder.OwnsOne(c => c.Veiculo, veiculo =>
        {
            veiculo.Property(p => p.Marca).HasColumnName("VeiculoMarca").HasColumnType("varchar(50)").IsRequired();
            veiculo.Property(p => p.Modelo).HasColumnName("VeiculoModelo").HasColumnType("varchar(50)").IsRequired();
            veiculo.Property(p => p.Ano).HasColumnName("VeiculoAno").HasColumnType("int").IsRequired();
        });

        // Exemplo de Value Object mapeado para campo JSON
        builder
            .Property(p => p.Proprietario)
            .HasColumnType("varchar(max)")
            .HasConversion(
                c => c.ToJson(),
                s => s.ToObject<Cotacao.DadosProprietario>())
            .IsRequired();

        // Exemplo de Value Object mapeado para campo JSON
        builder
            .Property(p => p.Condutor)
            .HasColumnType("varchar(max)")
            .HasConversion(
                c => c.ToJson(),
                s => s.ToObject<Cotacao.DadosCondutor>())
            .IsRequired();

        // Exemplo de mapeamento numa relação 1xN
        builder
            !.HasMany(s => s.Coberturas)
            !.WithOne()
            !.HasForeignKey("CoberturaId")
            !.OnDelete(DeleteBehavior.Cascade)
            !.IsRequired()
            !.Metadata
            !.PrincipalToDependent
            !.SetField("_coberturas");
    }
}
