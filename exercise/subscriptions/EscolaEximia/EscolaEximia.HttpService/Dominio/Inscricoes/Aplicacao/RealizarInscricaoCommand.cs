namespace EscolaEximia.HttpService.Dominio.Inscricoes.Aplicacao;

public class RealizarInscricaoCommand
{
    public string Aluno { get; set; }
    public string Responsavel { get; set; }
    public int Turma { get; set; }
}