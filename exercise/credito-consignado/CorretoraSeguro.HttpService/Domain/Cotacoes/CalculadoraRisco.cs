using CorretoraSeguro.HttpService.Domain.Sinistros;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes;

public class CalculadoraRisco
{
    public int Calcular(DateTime dataNascimento, HistoricoAcidentes historico, string uf)
    {
        var pontos = 0;
        var idade = DateTime.Today.Year - dataNascimento.Year;

        // Pontuação por idade
        pontos += idade switch
        {
            <= 25 => 15,
            <= 40 => 5,
            <= 60 => 3,
            _ => 10
        };

        // Pontuação por acidentes
        pontos += historico.QuantidadeAcidentes switch
        {
            0 => 0,
            1 => 10,
            2 => 20,
            _ => 30
        };

        // Pontuação por região
        pontos += ObterPontuacaoRegiao(uf);

        // Classificação final
        return pontos switch
        {
            <= 10 => 1,
            <= 25 => 2,
            <= 40 => 3,
            <= 55 => 4,
            _ => 5
        };
    }

    private int ObterPontuacaoRegiao(string uf) => uf switch
    {
        "SP" or "RJ" => 20, // Alto risco
        "MG" or "ES" => 10, // Médio risco
        _ => 5 // Baixo risco
    };
}