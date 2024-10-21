namespace EscolaEximia.HttpService.Dominio.Entidades;

public sealed class Inscricao
{
    public Inscricao(){}
    
    public Guid Id { get; set; }
    public string Aluno { get; set; }
    public string Responsavel { get; set; }
    public int Turma { get; set; }
    public bool Ativa { get; set; }
}