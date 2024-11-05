using CreditoConsignado.HttpService.Domain.SeedWork;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CreditoConsignado.HttpService.Domain.Propostas.Features.FluxoAprovacaoProposta;

public class IniciarAnaliseStep(PropostaRepository propostaRepository, UnitOfWork unitOfWork)
    : StepBodyAsync
{
    public int PropostaId { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var propostaMaybe = await propostaRepository.Obter(PropostaId, CancellationToken.None);
        if (propostaMaybe.HasNoValue)
            return ExecutionResult.Next();

        var proposta = propostaMaybe.Value;
        // Lógica de início de análise
        
        await unitOfWork.CommitAsync(CancellationToken.None);
        return ExecutionResult.Next();
    }
}

public class ValidarPropostaStep(PropostaRepository propostaRepository) : StepBodyAsync
{
    public int PropostaId { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var propostaMaybe = await propostaRepository.Obter(PropostaId, CancellationToken.None);
        if (propostaMaybe.HasNoValue)
            return ExecutionResult.Next();

        var proposta = propostaMaybe.Value;
        var workflowData = context.Workflow.Data as PropostaWorkflowData;

        // Lógica de validação
        workflowData.PossuiPendencias = false; // Resultado da validação
        workflowData.Aprovada = true; // Resultado da validação

        return ExecutionResult.Next();
    }
}

public class AdicionarPendenciaStep(
    PropostaRepository propostaRepository,
    UnitOfWork unitOfWork)
    : StepBodyAsync
{
    public int PropostaId { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var propostaMaybe = await propostaRepository.Obter(PropostaId, CancellationToken.None);
        if (propostaMaybe.HasNoValue)
            return ExecutionResult.Next();

        var proposta = propostaMaybe.Value;
        var resultado = proposta.AdicionarPendencia();
        
        if (resultado.IsFailure)
            return ExecutionResult.Next();

        await unitOfWork.CommitAsync(CancellationToken.None);
        return ExecutionResult.Next();
    }
}

public class AprovarPropostaStep(
    PropostaRepository propostaRepository,
    UnitOfWork unitOfWork)
    : StepBodyAsync
{
    public int PropostaId { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var propostaMaybe = await propostaRepository.Obter(PropostaId, CancellationToken.None);
        if (propostaMaybe.HasNoValue)
            return ExecutionResult.Next();

        var proposta = propostaMaybe.Value;
        var resultado = proposta.Aprovar();
        
        if (resultado.IsFailure)
            return ExecutionResult.Next();

        await unitOfWork.CommitAsync(CancellationToken.None);
        return ExecutionResult.Next();
    }
}

public class ReprovarPropostaStep(PropostaRepository propostaRepository, UnitOfWork unitOfWork)
    : StepBodyAsync
{
    public int PropostaId { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var propostaMaybe = await propostaRepository.Obter(PropostaId, CancellationToken.None);
        if (propostaMaybe.HasNoValue)
            return ExecutionResult.Next();

        var proposta = propostaMaybe.Value;
        var resultado = proposta.Reprovar();
        
        if (resultado.IsFailure)
            return ExecutionResult.Next();

        await unitOfWork.CommitAsync(CancellationToken.None);
        return ExecutionResult.Next();
    }
}