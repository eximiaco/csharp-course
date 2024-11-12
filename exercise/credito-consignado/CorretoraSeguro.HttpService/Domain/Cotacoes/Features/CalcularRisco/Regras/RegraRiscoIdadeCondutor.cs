namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularRisco.Regras;

public class RegraRiscoIdadeCondutor : IRegraRisco
{
    public RegraRiscoIdadeCondutor(Dictionary<int, int> idadePorPontos, int valorDefault)
    {
        IdadePorPontos = idadePorPontos;
        ValorDefault = valorDefault;
    }

    public Dictionary<int, int> IdadePorPontos { get; }
    public int ValorDefault { get; }

    public int Calcular(RegraRiscoContext contexto)
    {
        var idade = DateTime.Today.Year - contexto.DataNascimento.Year;
        if (IdadePorPontos.TryGetValue(idade, out var pontos))
            return pontos;
        return ValorDefault;
    }
}
