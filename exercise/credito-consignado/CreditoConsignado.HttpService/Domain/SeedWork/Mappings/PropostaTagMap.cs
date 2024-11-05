using CreditoConsignado.HttpService.Domain.Propostas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PropostaTagMap : IEntityTypeConfiguration<PropostaTag>
{
    public void Configure(EntityTypeBuilder<PropostaTag> builder)
    {
        builder.ToTable("PropostasTags");
        
        builder.HasKey(pt => new { pt.PropostaId, pt.TagId });

        builder.Property(pt => pt.DataVinculo)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.HasOne<Proposta>()
            .WithMany()
            .HasForeignKey(pt => pt.PropostaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Tag>()
            .WithMany()
            .HasForeignKey(pt => pt.TagId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}