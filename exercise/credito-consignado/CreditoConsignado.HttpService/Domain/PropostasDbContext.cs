using CreditoConsignado.HttpService.Domain.Agentes;
using CreditoConsignado.HttpService.Domain.Convenios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json;
using CreditoConsignado.HttpService.Domain.Propostas;

namespace CreditoConsignado.HttpService.Domain;

public class PropostasDbContext : DbContext
{
    public PropostasDbContext(DbContextOptions<PropostasDbContext> options) : base(options) { }

    public DbSet<Agente> Agentes { get; set; }
    public DbSet<Convenio> Convenios { get; set; }
    public DbSet<Proposta> Propostas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<PropostasDbContext>
{
    public PropostasDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PropostasDbContext>();
        var connectionString = "Server=localhost;Database=PropostasDB;User Id=sa;Password=SenhaForte123!;TrustServerCertificate=True;";
        
        optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });

        return new PropostasDbContext(optionsBuilder.Options);
    }
}