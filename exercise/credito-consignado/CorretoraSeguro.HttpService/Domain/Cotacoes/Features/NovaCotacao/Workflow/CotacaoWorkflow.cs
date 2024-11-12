using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow.Steps;
using WorkflowCore.Interface;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow;

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
            .Input(step => step.CotacaoId, data => data.CotacaoId);
            // .Then<EnviarEmailCotacaoStep>()
            // .Then<AguardarAprovacaoStep>()
            // .OnTimeout(TimeSpan.FromDays(5), (data, context) => 
            // {
            //     context.ExecuteActivity<CancelarCotacaoActivity>();
            //     return ExecutionResult.Next();
        // })
        // .Then<VerificarAprovacaoStep>()
        // .If(data => data.Aprovada)
        // .Then<GerarApoliceStep>()
        //.EndIf();
    }
}

public class CotacaoData
{
    public Guid CotacaoId { get; set; }
    public bool Aprovada { get; set; }
}