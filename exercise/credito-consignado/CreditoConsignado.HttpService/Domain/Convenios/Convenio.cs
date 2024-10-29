using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Convenios;

public sealed class Convenio : Entity<string>
{
    public string Nome { get; }
    public string[] OperacoesPermitidas { get; }
    public Dictionary<string, decimal> LimitesPorRegiao { get; }
}