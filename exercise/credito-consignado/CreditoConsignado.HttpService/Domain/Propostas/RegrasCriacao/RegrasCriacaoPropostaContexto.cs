using CreditoConsignado.HttpService.Domain.Agentes;

namespace CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;

public record RegrasCriacaoPropostaContexto(Proponente Proponente, CreditoSolicitado Credito, Agente Agente);
