using CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;
using Microsoft.EntityFrameworkCore;

namespace CreditoConsignado.HttpService.Domain.Propostas;

public sealed class PropostaRepository(PropostasDbContext propostasDbContext)
{
    public async Task<Boolean> ProponentePossuiPropostasAbertas(string cpf, CancellationToken cancellationToken)
    {
        return await propostasDbContext.Propostas
            .Where(c=> c.Proponente.Cpf == cpf)
            .AnyAsync(cancellationToken);
    }

    public async Task Adicionar(Proposta proposta, CancellationToken cancellationToken)
    {
        await propostasDbContext.Propostas.AddAsync(proposta, cancellationToken);
    }

    public async Task<IEnumerable<string>> ObterUFsDeAssinaturaHibrida(CancellationToken cancellationToken)
    {
        return new List<string>();
    }

    public Task<int> ObterProximoNumeroDeProposta(CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }

    public async Task<IEnumerable<IRegraCriacaoProposta>> ObterRegrasCriacaoNovaProposta(string convenioId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ObterBloqueiDeCpf(string cpf, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}