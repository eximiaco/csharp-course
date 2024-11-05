using System.Text.Json;
using CreditoConsignado.HttpService.Domain.Agentes;
using CreditoConsignado.HttpService.Domain.Convenios;
using CreditoConsignado.HttpService.Domain.Propostas;
using CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditoConsignado.HttpService.Domain.SeedWork.Mappings;

public class PropostaMap : IEntityTypeConfiguration<Proposta>
{
    public void Configure(EntityTypeBuilder<Proposta> builder)
    {
        builder.ToTable("Propostas");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.TipoAssinatura).HasColumnType("varchar(15)").IsRequired();
        
        #region Controle de Concorrência
        builder.Property(p => p.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();
        #endregion
        

        #region Mapeamento de relacionamentos 1-N
        builder.Property(p => p.ConvenioId)
            .HasColumnType("varchar(36)")
            .IsRequired();
        
        builder.Property(p => p.AgenteId)
            .HasColumnType("varchar(36)")
            .IsRequired();

        builder.HasOne<Convenio>()
            .WithMany()
            .HasForeignKey(p => p.ConvenioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Agente>()
            .WithMany()
            .HasForeignKey(p => p.AgenteId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        
        #region Mapeamento de Anexos
        builder.HasMany(p => p.Anexos)
            .WithOne()
            .HasForeignKey("PropostaId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(p => p.Anexos)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_anexos");
        #endregion

        #region Mapeamento de Tags
        builder.HasMany(p => p.Tags)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PropostasTags",
                x => x
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .OnDelete(DeleteBehavior.Restrict),
                x => x
                    .HasOne<Proposta>()
                    .WithMany()
                    .HasForeignKey("PropostaId")
                    .OnDelete(DeleteBehavior.Cascade),
                x =>
                {
                    x.HasKey("PropostaId", "TagId");
                    x.Property<DateTime>("DataVinculo")
                        .HasDefaultValueSql("GETUTCDATE()");
                });

        builder.Navigation(p => p.Tags)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_tags");
        #endregion
        
        builder.OwnsOne(p => p.Proponente, proponente =>
        {
            proponente.Property(p => p.Cpf).HasColumnName("ProponenteCpf").HasColumnType("varchar(20)").IsRequired();
            proponente.Property(p => p.CpfBloqueado).HasColumnName("ProponenteCpfBloqueado").HasColumnType("bit").IsRequired();

            proponente.OwnsOne(p => p.Contato, contato =>
            {
                contato.Property(c => c.DDD).HasColumnName("ProponenteDDD").HasColumnType("varchar(3)").IsRequired();
                contato.Property(c => c.DDD).HasColumnName("ProponenteTelefone").HasColumnType("varchar(30)").IsRequired();
            });

            proponente.OwnsOne(p => p.Residencial, residencial =>
            {
                residencial.Property(c => c.Cep).HasColumnName("ProponenteCep").HasColumnType("varchar(15)").IsRequired();
                residencial.Property(c => c.Rua).HasColumnName("ProponenteRua").HasColumnType("varchar(100)").IsRequired();
                residencial.Property(c => c.Bairro).HasColumnName("ProponenteBairro").HasColumnType("varchar(50)").IsRequired();
                residencial.Property(c => c.Cidade).HasColumnName("ProponenteCidade").HasColumnType("varchar(150)").IsRequired();
                residencial.Property(c => c.Estado).HasColumnName("ProponenteEstado").HasColumnType("varchar(2)").IsRequired();
            });
        });

        builder.OwnsOne(p => p.Credito, credito =>
        {
            credito.Property(c => c.Valor).HasColumnName("CreditoValor").HasColumnType("numeric(15,2)").IsRequired();
            credito.Property(c => c.Parcelamento).HasColumnName("CreditoParcelamento").HasColumnType("int").IsRequired();
        });

        #region Soft Delete
        builder.Property(p => p.DataExclusao)
            .HasColumnType("datetime2")
            .IsRequired(false);

        builder.HasQueryFilter(p => p.DataExclusao == null);
        #endregion

        builder.Property<ISituacaoProposta>("_situacao")
            .HasColumnType("varchar(max)")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
                v => JsonSerializer.Deserialize<ISituacaoProposta>(v, new JsonSerializerOptions { WriteIndented = true })!)
            .IsRequired();

        builder.Navigation("_situacao")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
