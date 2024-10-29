using CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;
using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas;

public sealed class Proposta : Entity<int>
{
    public Proposta(int id, Proponente proponente, string tipoAssinatura, CreditoSolicitado credito) : base(id)
    {
        Proponente = proponente;
        TipoAssinatura = tipoAssinatura;
        Credito = credito;
    }

    public Proponente Proponente { get; }
    public string TipoAssinatura { get; }
    public CreditoSolicitado Credito { get; }

    public static Result<Proposta> Criar(
        int id,
        Proponente proponente,
        CreditoSolicitado credito,
        string tipoAssinatura,
        IEnumerable<IRegraCriacaoProposta> regrasCriacao)
    {
        foreach (var regra in regrasCriacao)
        {
            var resultado = regra.Validar(proponente, credito);
            if (resultado.IsFailure)
                return Result.Failure<Proposta>(resultado.Error);
        }
        return new Proposta(id, proponente, tipoAssinatura, credito);
    }
}

public record Proponente(string Cpf, Telefone Contato, Endereco Residencial, bool CpfBloqueado);
public record Telefone(string DDD, string Numero);
public record Endereco(string Cep, string Rua, string Bairro, string Cidade, string Estado);
public record CreditoSolicitado(decimal Valor, int Parcelamento);