using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes;

public sealed class CoberturaCalculada : Entity<Guid>
{
    private CoberturaCalculada() { } // EF

    public CoberturaCalculada(Guid id, ETipoCobertura tipo, decimal valor) : base(id)
    {
        Tipo = tipo;
        Valor = valor;
    }

    public ETipoCobertura Tipo { get; private set; }
    public decimal Valor { get; private set; }

    public void AtualizarValor(decimal novoValor)
    {
        Valor = novoValor;
    }

    internal decimal CalcularValor(decimal valorBase) => Valor * valorBase;
} 