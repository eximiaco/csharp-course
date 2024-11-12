using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.FluxoNovaCotacao.Steps;
using WorkflowCore.Interface;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.FluxoNovaCotacao;

public class CotacaoWorkflow : IWorkflow<CotacaoData>
{
    public string Id => "CotacaoWorkflow";
    public int Version => 1;

    public void Build(IWorkflowBuilder<CotacaoData> builder)
    {
        builder
            .StartWith<CalcularRiscoStep>()
            .Input(step => step.CotacaoId, data => data.CotacaoId)
            .Then<CalcularBaseSeguroStep>()
            .Input(step => step.CotacaoId, data => data.CotacaoId)
            .Then<CalcularValorFinalStep>()
            .Input(step => step.CotacaoId, data => data.CotacaoId)
            .Then<EnviarEmailCotacaoStep>()
            .Input(step => step.CotacaoId, data => data.CotacaoId)
            .Parallel()
                .Do(then => then
                    .StartWith<NoOpStep>()
                    .WaitFor("AprovacaoCotacao", (data, context) => data.CotacaoId.ToString())
                    .Then<AtualizarAprovacaoStep>()
                    .Input(step => step.CotacaoId, data => data.CotacaoId))
                .Do(then => then
                    .StartWith<NoOpStep>()
                    .Delay(data => TimeSpan.FromDays(5))
                    .Then<CancelarCotacaoStep>()
                    .Input(step => step.CotacaoId, data => data.CotacaoId))
            .Join()
                .CancelCondition(data => !data.Aprovado, true)
            .Then<GerarApoliceStep>()
            .Input(step => step.CotacaoId, data => data.CotacaoId);
    }
}

public class CotacaoData
{
    public Guid CotacaoId { get; set; } 
    public bool Aprovado { get; set; }
}