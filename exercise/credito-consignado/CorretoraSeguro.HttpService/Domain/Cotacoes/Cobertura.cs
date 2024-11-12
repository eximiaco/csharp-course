namespace CorretoraSeguro.HttpService.Domain.Cotacoes;

public class Cobertura
{
    public TipoCobertura Tipo { get; private set; }
    public decimal Valor { get; private set; }

    private Cobertura() { } // EF

    public Cobertura(TipoCobertura tipo, decimal valor)
    {
        Tipo = tipo;
        Valor = valor;
    }

    public void AtualizarValor(decimal novoValor)
    {
        Valor = novoValor;
    }

    internal decimal CalcularValor()
    {
        return Tipo switch
        {
            TipoCobertura.Basica => Valor * 0.03m,      // 3%
            TipoCobertura.Roubo => Valor * 0.04m,       // 4%
            TipoCobertura.Vidros => Valor * 0.015m,     // 1.5%
            _ => 0
        };
    }
} 

public enum TipoCobertura
{
    Basica,
    Roubo,
    Vidros
}