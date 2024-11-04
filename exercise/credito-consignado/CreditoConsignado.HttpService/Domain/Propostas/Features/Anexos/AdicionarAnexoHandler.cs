using CreditoConsignado.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas.Features.Anexos;

public sealed class AdicionarAnexoHandler(
    PropostaRepository propostaRepository,
    UnitOfWork unitOfWork)
{
    public async Task<Result> ExecutarAsync(AdicionarAnexoCommand command, CancellationToken cancellationToken)
    {
        var propostaMaybe = await propostaRepository.Obter(command.PropostaId, cancellationToken);
        if (propostaMaybe.HasNoValue)
            return Result.Failure("Proposta não encontrada");
        
        var resultado = propostaMaybe.Value.AdicionarAnexo(command.Path);
        if (resultado.IsFailure)
            return Result.Failure(resultado.Error);

        await unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Success();
    }
}