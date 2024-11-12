namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco.Regras;

public class RegraRiscoPontuacaoPorAcidentes : IRegraRisco
{
    public RegraRiscoPontuacaoPorAcidentes(Dictionary<int, int> acidentesPorPontos, int valorDefault)
    {
        AcidentesPorPontos = acidentesPorPontos;
        ValorDefault = valorDefault;
    }

    public Dictionary<int, int> AcidentesPorPontos { get; }
    public int ValorDefault { get; }

    public int Calcular(RegraRiscoContext contexto)
    {
        if (AcidentesPorPontos.TryGetValue(contexto.Historico.QuantidadeAcidentes, out var pontos))
            return pontos;
        return ValorDefault;
    }
}
