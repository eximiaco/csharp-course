namespace EscolaEximia.HttpService.Dominio.Entidades;

public class Aluno
{
    public string Cpf { get; set; } 
    public ESexo Sexo { get; set; }
    public int Idade { get; set; }
}

public enum ESexo { Masculino, Feminino, Outro}