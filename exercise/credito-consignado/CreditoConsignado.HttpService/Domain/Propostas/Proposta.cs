using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas;

public sealed class Proposta : Entity<int>
{
    public Proposta(int id, Proponente proponente, string tipoAssinatura) : base(id)
    {
        Proponente = proponente;
        TipoAssinatura = tipoAssinatura;
    }

    public Proponente Proponente { get; }
    public string TipoAssinatura { get; }
    public static Result<Proposta> Criar(int id, Proponente proponente, string tipoAssinatura)
    {
        return new Proposta(id, proponente, tipoAssinatura);
    }
}

public record Proponente(string Cpf, Telefone Contato, Endereco Residencial);
public record Telefone(string DDD, string Numero);
public record Endereco(string Cep, string Rua, string Bairro, string Cidade, string Estado);