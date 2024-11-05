using CreditoConsignado.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CreditoConsignado.HttpService.Domain.Propostas.Features.SimularNovoValor;

public sealed class SimularNovoValorHandler(
    PropostaRepository propostaRepository,
    UnitOfWork unitOfWork)
{
    
    public async Task<Result> ExecutarAsync(SimularNovoValorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var propostaMaybe = await propostaRepository.Obter(command.PropostaId, cancellationToken);
            if (propostaMaybe.HasNoValue)
                return Result.Failure("Proposta não encontrada");
            
            propostaMaybe.Value.SimularNovoValor(command.ValorSolicitado, command.PropostaId);

            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failure("A proposta foi alterada por outro usuário. Por favor, atualize seus dados e tente novamente.");
        }
    }
}