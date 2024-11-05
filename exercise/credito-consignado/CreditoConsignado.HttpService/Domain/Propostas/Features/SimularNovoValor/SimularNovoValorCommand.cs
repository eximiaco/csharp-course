public sealed record SimularNovoValorCommand(
    int PropostaId, 
    decimal ValorSolicitado,
    int QuantidadeParcelas);