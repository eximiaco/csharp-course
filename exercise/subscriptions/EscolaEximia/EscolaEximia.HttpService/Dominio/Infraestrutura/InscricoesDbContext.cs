using EscolaEximia.HttpService.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EscolaEximia.HttpService.Dominio.Infraestrutura;

public class InscricoesDbContext: DbContext
{
    public const string DEFAULT_SCHEMA = "inscricoes";
    
    public InscricoesDbContext(DbContextOptions<InscricoesDbContext> options) : base(options) { }
    
    public DbSet<Inscricao> Inscricoes { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Aluno> Alunos { get; set; }

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
        //modelBuilder.ApplyConfiguration(new InscricoesConfigurations());
        //modelBuilder.ApplyConfiguration(new TurmasConfigurations());
    }
}