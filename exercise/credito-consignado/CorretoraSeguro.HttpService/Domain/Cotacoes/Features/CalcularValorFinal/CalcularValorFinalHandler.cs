using WorkflowCore.Exceptions;
using CorretoraSeguro.HttpService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularValorFinal
{
    public class CalcularValorFinalHandler
    {
        private readonly PropostasDbContext _dbContext;

        public CalcularValorFinalHandler(PropostasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CalcularValorFinalCommand command, CancellationToken cancellationToken)
        {
            var cotacao = await _dbContext.Cotacoes
                .FirstOrDefaultAsync(x => x.Id == command.CotacaoId, cancellationToken);
            
            if (cotacao == null)
                throw new NotFoundException("Cotação não encontrada");

            cotacao.CalcularValorFinal();

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 