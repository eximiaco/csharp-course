namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao;

public record NovaCotacaoCommand(
    NovaCotacaoCommand.VeiculoRecord Veiculo,
    NovaCotacaoCommand.ProprietarioRecord Proprietario,
    NovaCotacaoCommand.CondutorRecord Condutor,
    List<string> Coberturas
)
{
    public record VeiculoRecord(
        string Marca,
        string Modelo,
        int Ano
    );

    public record ProprietarioRecord(
        string Cpf,
        string Nome,
        DateTime DataNascimento,
        EnderecoRecord Residencia
    );

    public record CondutorRecord(
        string Cpf,
        DateTime DataNascimento,
        EnderecoRecord Residencia
    );

    public record EnderecoRecord(
        string Cep,
        string Cidade,
        string UF
    );
}