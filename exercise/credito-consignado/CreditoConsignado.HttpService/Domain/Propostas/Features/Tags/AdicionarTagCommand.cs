public sealed record AdicionarTagCommand
{
    public int PropostaId { get; init; }
    public int TagId { get; init; }
}