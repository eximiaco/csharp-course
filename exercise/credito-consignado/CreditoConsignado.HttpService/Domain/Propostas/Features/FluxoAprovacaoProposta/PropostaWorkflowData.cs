namespace CreditoConsignado.HttpService.Domain.Propostas.Features.FluxoAprovacaoProposta;

public class PropostaWorkflowData
{
    public int PropostaId { get; set; }
    public bool PossuiPendencias { get; set; }
    public bool Aprovada { get; set; }
}