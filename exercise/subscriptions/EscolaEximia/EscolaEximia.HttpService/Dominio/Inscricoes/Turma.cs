namespace EscolaEximia.HttpService.Dominio.Inscricoes;

public record Turma
{
    public int Id { get; }
    public int Vagas { get; private set; }
    public bool Masculino { get; }
    public bool Feminino { get; }
    public int LimiteIdade { get; }

    private Turma(){}
    
    public Turma(int id, int vagas, bool masculino, bool feminino, int limiteIdade)
    {
        Id = id;
        Vagas = vagas;
        Masculino = masculino;
        Feminino = feminino;
        LimiteIdade = limiteIdade;
    }

    public void DecrementarVagas()
    {
        if (Vagas > 0)
        {
            Vagas--;
        }
    }
}
