using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;

public sealed class RegraPorConvenio : Entity<Guid>
{
    public string ConvenioId { get; }
    public string Nome { get; }
    public IRegraCriacaoProposta Regra { get; }
    public bool Ativa { get; }
}