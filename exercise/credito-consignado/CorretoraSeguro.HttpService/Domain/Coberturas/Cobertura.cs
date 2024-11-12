using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

public class Cobertura : Entity<Guid>
{
    public string Nome { get; private set; }
    public TipoCobertura Tipo { get; private set; }
    public decimal PercentualCalculo { get; private set; }
    public bool TaxaFixa { get; private set; }
    public decimal? ValorFixo { get; private set; }

    private Cobertura() { }

    private Cobertura(string nome, decimal percentualCalculo, bool taxaFixa = false, decimal? valorFixo = null)
    {
        Nome = nome;
        PercentualCalculo = percentualCalculo;
        TaxaFixa = taxaFixa;
        ValorFixo = valorFixo;
    }

    public static Cobertura Criar(string nome, decimal percentualCalculo, bool taxaFixa = false, decimal? valorFixo = null)
        => new(nome, percentualCalculo, taxaFixa, valorFixo);

    public decimal CalcularValor(decimal valorBase)
        => TaxaFixa ? ValorFixo ?? 0 : valorBase * PercentualCalculo;
} 