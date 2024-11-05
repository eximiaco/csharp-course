using CSharpFunctionalExtensions;

public sealed class Tag : Entity<int>
{
    public string Nome { get; }
    
    private Tag() {}
    private Tag(int id, string nome) : base(id)
    {
        Nome = nome;
    }

    public static Tag Criar(int id, string nome)
    {
        return new Tag(id, nome);
    }
}