namespace CorretoraSeguro.HttpService.Domain.Cotacoes;

public class CalculadoraSeguro
{
    private static readonly Dictionary<string, decimal> CustosBase = new()
    {
        ["ROUBO"] = 0.03m,
        ["COLISAO"] = 0.04m,
        ["TERCEIROS"] = 0.015m
    };

    private static readonly Dictionary<int, decimal> AjustesRisco = new()
    {
        [1] = 1.00m,
        [2] = 1.05m,
        [3] = 1.10m,
        [4] = 1.20m,
        [5] = 1.30m
    };

    public decimal Calcular(decimal valorFipe, int nivelRisco, List<string> coberturas)
    {
        var valorTotal = 0m;
        var ajusteRisco = AjustesRisco[nivelRisco];

        foreach (var cobertura in coberturas)
        {
            if (cobertura == "RESIDENCIAL")
            {
                valorTotal += 100m; // Valor fixo
                continue;
            }

            if (CustosBase.TryGetValue(cobertura, out var custoBase))
            {
                var valorCobertura = valorFipe * custoBase * ajusteRisco;
                valorTotal += valorCobertura;
            }
        }

        return Math.Round(valorTotal, 2);
    }
}