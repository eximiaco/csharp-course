using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas;

public sealed class Anexo : Entity<Guid>
{
    private Anexo() { }
    public Anexo(Guid id, string path, bool estaValido)
    {
        Id = id;
        Path = path;
        EstaValido = estaValido;
    }
    
    public string Path { get; }
    public bool EstaValido { get; }
}