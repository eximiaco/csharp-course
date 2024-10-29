using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;

public sealed class RegraCpfBloqueado : IRegraCriacaoProposta
{
    public Result Validar(Proponente proponente, CreditoSolicitado credito)
    {
        return proponente.CpfBloqueado 
            ? Result.Failure("CPF está bloqueado") 
            : Result.Success();
    }
}