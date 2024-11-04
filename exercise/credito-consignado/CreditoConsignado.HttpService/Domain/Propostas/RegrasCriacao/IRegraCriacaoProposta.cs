using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;

public interface IRegraCriacaoProposta
{
    Result Validar(Proponente proponente, CreditoSolicitado credito);
}