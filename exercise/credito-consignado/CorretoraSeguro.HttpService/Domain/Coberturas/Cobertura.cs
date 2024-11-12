using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

public class Cobertura : Entity<Guid>
{
    private Cobertura() { }

    private Cobertura(Guid id, string nome, decimal percentualCalculo, bool taxaFixa = false, decimal? valorFixo = null) : base(id)
    {
        Nome = nome;
        PercentualCalculo = percentualCalculo;
        TaxaFixa = taxaFixa;
        ValorFixo = valorFixo;
    }

    public string Nome { get; private set; } = string.Empty;
    public ETipoCobertura Tipo { get; private set; }
    public decimal PercentualCalculo { get; private set; }
    public bool TaxaFixa { get; private set; }
    public decimal? ValorFixo { get; private set; }

    public static Cobertura Criar(string nome, decimal percentualCalculo, bool taxaFixa = false, decimal? valorFixo = null)
        => new(Guid.NewGuid(), nome, percentualCalculo, taxaFixa, valorFixo);

    public decimal CalcularValor(decimal valorBase)
        => TaxaFixa ? ValorFixo ?? 0 : valorBase * PercentualCalculo;
} 