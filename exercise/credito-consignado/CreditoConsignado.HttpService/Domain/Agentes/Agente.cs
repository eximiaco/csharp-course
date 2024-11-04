using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Agentes;

public sealed class Agente : Entity<string>
{
    public string Nome { get; }
    public bool Ativo { get; }
}