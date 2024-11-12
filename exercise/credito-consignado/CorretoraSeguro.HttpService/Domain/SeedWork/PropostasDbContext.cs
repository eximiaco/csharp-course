using CorretoraSeguro.HttpService.Domain.Cotacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CorretoraSeguro.HttpService.Domain.SeedWork;

public class PropostasDbContext : DbContext
{
    public PropostasDbContext(DbContextOptions<PropostasDbContext> options) : base(options) { }

    public DbSet<Cotacao> Cotacoes { get; set; }
    public DbSet<Cobertura> Coberturas { get; set; }

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