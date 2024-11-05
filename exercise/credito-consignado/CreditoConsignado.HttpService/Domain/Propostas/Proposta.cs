using CreditoConsignado.HttpService.Domain.Agentes;
using CreditoConsignado.HttpService.Domain.Convenios;
using CreditoConsignado.HttpService.Domain.Propostas.RegrasCriacao;
using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas;

public sealed class Proposta : Entity<int>
{
    private readonly List<Anexo> _anexos;
    private readonly List<Tag> _tags;

    public string ConvenioId { get; }
    public string AgenteId { get; }
    public Proponente Proponente { get; }
    public string TipoAssinatura { get; }
    public CreditoSolicitado Credito { get; }
    public IReadOnlyCollection<Anexo> Anexos => _anexos.AsReadOnly();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    public byte[] RowVersion { get; private set; }
    public DateTime? DataExclusao { get; private set; }
    public bool Excluido => DataExclusao.HasValue;

    private Proposta(){}
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
        _anexos = new List<Anexo>();
        _tags = new List<Tag>();
        RowVersion = Array.Empty<byte>();
        DataExclusao = null;
    }

    public Result AdicionarAnexo(string path)
    {
        var anexo = new Anexo(Guid.NewGuid(), path, false);
        _anexos.Add(anexo);
        return Result.Success();
    }

    public Result RemoverAnexo(Guid anexoId)
    {
        var anexo = _anexos.FirstOrDefault(a => a.Id == anexoId);
        if (anexo == null)
            return Result.Failure("Anexo não encontrado");

        _anexos.Remove(anexo);
        return Result.Success();
    }

    public Result AdicionarTag(Tag tag)
    {
        if (_tags.Any(t => t.Id == tag.Id))
            return Result.Failure("Tag já adicionada à proposta");

        _tags.Add(tag);
        return Result.Success();
    }

    public void SimularNovoValor(decimal commandValorSolicitado, int commandPropostaId)
    {
        throw new NotImplementedException();
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

    public void Excluir()
    {
        DataExclusao = DateTime.UtcNow;
    }
}

public record Proponente(string Cpf, Telefone Contato, Endereco Residencial, bool CpfBloqueado);
public record Telefone(string DDD, string Numero);
public record Endereco(string Cep, string Rua, string Bairro, string Cidade, string Estado);
public record CreditoSolicitado(decimal Valor, int Parcelamento);