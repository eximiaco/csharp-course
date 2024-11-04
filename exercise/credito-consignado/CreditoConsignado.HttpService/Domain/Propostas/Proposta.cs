using CreditoConsignado.HttpService.Domain.Agentes;
using CreditoConsignado.HttpService.Domain.Convenios;
using CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;
using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas;

public sealed class Proposta : Entity<int>
{
    public string ConvenioId { get; }
    public string AgenteId { get; }
    public Proponente Proponente { get; }
    public string TipoAssinatura { get; }
    public CreditoSolicitado Credito { get; }

    private Proposta(
        int id, 
        string convenioId,
        string agenteId,
        Proponente proponente, 
        string tipoAssinatura, 
        CreditoSolicitado credito) : base(id)
    {
        ConvenioId = convenioId;
        AgenteId = agenteId;
        Proponente = proponente;
        TipoAssinatura = tipoAssinatura;
        Credito = credito;
    }

    public static Result<Proposta> Criar(
        int id,
        Convenio convenio,
        Agente agente,
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
        
        return Result.Success(new Proposta(
            id, 
            convenio.Id,
            agente.Id, 
            proponente, 
            tipoAssinatura, 
            credito));
    }
}

public record Proponente(string Cpf, Telefone Contato, Endereco Residencial, bool CpfBloqueado);
public record Telefone(string DDD, string Numero);
public record Endereco(string Cep, string Rua, string Bairro, string Cidade, string Estado);
public record CreditoSolicitado(decimal Valor, int Parcelamento);