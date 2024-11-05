public sealed class PropostaTag
{
    public int PropostaId { get; }
    public int TagId { get; }
    public DateTime DataVinculo { get; }
    
    private PropostaTag(int propostaId, int tagId)
    {
        PropostaId = propostaId;
        TagId = tagId;
        DataVinculo = DateTime.UtcNow;
    }

    public static PropostaTag Criar(int propostaId, int tagId)
    {
        return new PropostaTag(propostaId, tagId);
    }
}