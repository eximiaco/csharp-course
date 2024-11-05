using WorkflowCore.Interface;

namespace CreditoConsignado.HttpService.Domain.Propostas.Features.FluxoAprovacaoProposta;

public class PropostaWorkflow : IWorkflow<PropostaWorkflowData>
{
    public string Id => "PropostaWorkflow";
    public int Version => 1;

    public void Build(IWorkflowBuilder<PropostaWorkflowData> builder)
    {
        builder
            .StartWith<IniciarAnaliseStep>()
                .Input(step => step.PropostaId, data => data.PropostaId)
            .Then<ValidarPropostaStep>()
                .Input(step => step.PropostaId, data => data.PropostaId)
            .If(data => data.PossuiPendencias)
                .Do(x => x
                    .StartWith<AdicionarPendenciaStep>()
                    .Input(step => step.PropostaId, data => data.PropostaId))
            .If(data => !data.PossuiPendencias && data.Aprovada)
                .Do(x => x
                    .StartWith<AprovarPropostaStep>()
                    .Input(step => step.PropostaId, data => data.PropostaId))
            .If(data => !data.PossuiPendencias && !data.Aprovada)
                .Do(x => x
                    .StartWith<ReprovarPropostaStep>()
                    .Input(step => step.PropostaId, data => data.PropostaId));
    }
}