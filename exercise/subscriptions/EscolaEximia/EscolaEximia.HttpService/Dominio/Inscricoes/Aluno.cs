namespace EscolaEximia.HttpService.Dominio.Inscricoes;

public record Aluno(string Cpf, ESexo Sexo, int Idade);

public enum ESexo { Masculino, Feminino, Outro}
