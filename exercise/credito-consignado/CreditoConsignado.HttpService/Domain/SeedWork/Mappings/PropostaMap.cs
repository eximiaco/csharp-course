﻿using CreditoConsignado.HttpService.Domain.Propostas;
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
    }
}
