namespace CorretoraSeguro.HttpService.Domain.Sinistros;

public record HistoricoAcidentes(int QuantidadeAcidentes);
public interface IHistoricoAcidentesService
{
    Task<HistoricoAcidentes> ObterHistorico(string condutorCpf);
}