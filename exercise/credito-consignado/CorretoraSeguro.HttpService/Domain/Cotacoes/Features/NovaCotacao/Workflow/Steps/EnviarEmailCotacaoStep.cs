using WorkflowCore.Exceptions;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow.Steps;

public class EnviarEmailCotacaoStep : IStepBody
{
    public Guid CotacaoId { get; set; }
    private readonly IEmailService _emailService;
    private readonly CotacoesRepository _cotacaoRepository;

    public EnviarEmailCotacaoStep(IEmailService emailService, CotacoesRepository cotacaoRepository)
    {
        _emailService = emailService;
        _cotacaoRepository = cotacaoRepository;
    }

    public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var cotacao = await _cotacaoRepository.ObterPorIdAsync(CotacaoId, CancellationToken.None);
           if(cotacao.HasNoValue) 
               throw new NotFoundException($"Cotação {CotacaoId} não encontrada");
        
        await _emailService.EnviarEmailCotacao(cotacao.Value);
        return ExecutionResult.Next();
    }
} 