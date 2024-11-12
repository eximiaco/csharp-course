using CorretoraSeguro.HttpService.Domain.Sinistros;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco.Regras;

public record RegraRiscoContext(DateTime DataNascimento, HistoricoAcidentes Historico, string Uf);
