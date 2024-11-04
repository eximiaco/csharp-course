using CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;
using CreditoConsignado.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CreditoConsignado.HttpService.Domain.Propostas;

public sealed class PropostaRepository(PropostasDbContext propostasDbContext)
{
    public async Task<bool> ProponentePossuiPropostasAbertasAsync(string cpf, CancellationToken cancellationToken)
    {
        return await propostasDbContext
            .Propostas
            .Where(c=> c.Proponente.Cpf == cpf)
            .AnyAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task Adicionar(Proposta proposta, CancellationToken cancellationToken)
    {
        await propostasDbContext.Propostas.AddAsync(proposta, cancellationToken);
    }

    public async Task<IEnumerable<string>> ObterUFsDeAssinaturaHibridaAsync(CancellationToken cancellationToken)
    {
        return new List<string>();
    }

    public Task<int> ObterProximoNumeroDeProposta(CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }

    public async Task<IEnumerable<IRegraCriacaoProposta>> ObterRegrasCriacaoNovaPropostaAsync(string convenioId, CancellationToken cancellationToken)
    {
        var sql = @"SELECT Regra
                    FROM RegrasCriacaoProposta";

        var regras = await propostasDbContext.Database.GetDbConnection().QueryAsync<string>(sql).WaitAsync(cancellationToken).ConfigureAwait(false);
        return regras.Select(r => r.ToNameTypeObject<IRegraCriacaoProposta>());
    }

    public async Task<bool> ObterBloqueioDeCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Maybe<Proposta>> Obter(int id, CancellationToken cancellationToken)
    {
        return await propostasDbContext
            .Propostas
            .Include(c=> c.Anexos)
            .FirstAsync(c=> c.Id == id, cancellationToken);
    }
}