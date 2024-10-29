using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas.Features.NovaProposta;

public class NovaPropostaHandler
{
    public Task<Result<int>> Executar(NovaPropostaCommand command, CancellationToken token)
    {
        return Task.FromResult(Result.Success(0));
    }
}