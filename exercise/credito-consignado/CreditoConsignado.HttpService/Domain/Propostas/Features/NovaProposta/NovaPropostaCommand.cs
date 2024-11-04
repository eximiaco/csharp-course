namespace CreditoConsignado.HttpService.Domain.Propostas.Features.NovaProposta;

public record NovaPropostaCommand(
    string ConveniadaId,
    decimal ValorSolicitado,
    int QuantidadeParcelas,
    string AgenteId,
    DadosProponente Proponente
);

public record DadosProponente(
    string Cpf,
    DateTime DataNascimento,
    string Endereco,
    string UF,
    string Telefone,
    string Email,
    decimal Rendimento
);