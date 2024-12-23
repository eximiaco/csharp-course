using EscolaEximia.HttpService.Dominio.Inscricoes;
using EscolaEximia.HttpService.Dominio.Inscricoes.Infra.Mapeamento;
using EscolaEximia.HttpService.Dominio.Regras;
using EscolaEximia.HttpService.Dominio.Regras.infra.Mapeamento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EscolaEximia.HttpService.Dominio;

public class InscricoesDbContext: DbContext
{
    public const string DEFAULT_SCHEMA = "inscricoes";
    
    public InscricoesDbContext(DbContextOptions<InscricoesDbContext> options) : base(options) { }
    
    public DbSet<Inscricao> Inscricoes { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<RegraPorTurma> RegrasPorTurma { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if ((item.State == EntityState.Modified || item.State == EntityState.Added)
                    && item.Properties.Any(c => c.Metadata.Name == "DataUltimaAlteracao"))
                    item.Property("DataUltimaAlteracao").CurrentValue = DateTime.UtcNow;

                if (item.State == EntityState.Added)
                    if (item.Properties.Any(c => c.Metadata.Name == "DataCadastro") && item.Property("DataCadastro").CurrentValue.GetType() != typeof(DateTime))
                        item.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
            }
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch (DbUpdateException e)
        {
            throw new Exception();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InscricaoConfiguration());
        modelBuilder.ApplyConfiguration(new TurmaConfiguration());
        modelBuilder.ApplyConfiguration(new AlunoConfiguration());
        modelBuilder.ApplyConfiguration(new RegraConfiguration());
    }
}

public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<InscricoesDbContext>
{
    public InscricoesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InscricoesDbContext>();
        var connectionString = "Server=localhost;Database=InscricoesDB;User Id=sa;Password=SenhaForte123!;TrustServerCertificate=True;";
        
        optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });

        return new InscricoesDbContext(optionsBuilder.Options);
    }
}
