using Microsoft.EntityFrameworkCore;

namespace EscolaEximia.HttpService.Dominio.Regras.infra;

public class RegraPorTurmaRepository(InscricoesDbContext context)
{
    public async Task<IEnumerable<RegraPorTurma>> ObterRegrasPorTurmaAsync(int turmaId)
    {
        return await context.RegrasPorTurma
            .Where(r => r.TurmaId == turmaId)
            .ToListAsync();
    }
}
