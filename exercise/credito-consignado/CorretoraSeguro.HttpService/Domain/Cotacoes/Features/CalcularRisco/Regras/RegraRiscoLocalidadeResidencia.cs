namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco.Regras;

public class RegraRiscoLocalidadeResidencia : IRegraRisco
{
    public RegraRiscoLocalidadeResidencia(Dictionary<string, int> localidadePorPontos, int valorDefault)
    {
        LocalidadePorPontos = localidadePorPontos;
        ValorDefault = valorDefault;
    }

    public Dictionary<string, int> LocalidadePorPontos { get; }
    public int ValorDefault { get; }

    public int Calcular(RegraRiscoContext contexto)
    {
        if (LocalidadePorPontos.TryGetValue(contexto.Uf, out var pontos))
            return pontos;
        return ValorDefault;
    }
}
