using CorretoraSeguro.HttpService.Domain.Cotacoes;
using CorretoraSeguro.HttpService.Domain.SeedWork.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CorretoraSeguro.HttpService.Domain.SeedWork;

public class PropostasDbContext : DbContext
{
    public PropostasDbContext(DbContextOptions<PropostasDbContext> options) : base(options) { }

    public DbSet<Cotacao> Cotacoes { get; set; }
    public DbSet<Cobertura> Coberturas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CoberturasEFMap());
        modelBuilder.ApplyConfiguration(new CotacoesEFMap());
        modelBuilder.ApplyConfiguration(new CoberturaCalculadaEFMap());
    }
}
