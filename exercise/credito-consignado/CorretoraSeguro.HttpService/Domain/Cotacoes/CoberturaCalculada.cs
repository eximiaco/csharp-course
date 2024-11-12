using CorretoraSeguro.HttpService.Domain.SeedWork;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes;

public sealed class CoberturaCalculada
{
    public TipoCobertura Tipo { get; private set; }
    public decimal Valor { get; private set; }

    private CoberturaCalculada() { } // EF

    public CoberturaCalculada(TipoCobertura tipo, decimal valor)
    {
        Tipo = tipo;
        Valor = valor;
    }

    public void AtualizarValor(decimal novoValor)
    {
        Valor = novoValor;
    }

    internal decimal CalcularValor(decimal valorBase) => Valor * valorBase;
} 