public sealed record AdicionarAnexoCommand
{
    public int PropostaId { get; init; }
    public string Path { get; init; }
}
