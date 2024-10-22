using CSharpFunctionalExtensions;

namespace EscolaEximia.HttpService.Dominio.Inscricoes;

public sealed class Inscricao : Entity<Guid>
{
    private Inscricao(Guid id, string alunoCpf, string responsavel, Turma turma, bool ativa)
    {
        Id = id;
        AlunoCpf = alunoCpf;
        Responsavel = responsavel;
        Turma = turma;
        Ativa = ativa;
    }
    
    public Guid Id { get; }
    public string AlunoCpf { get; }
    public string Responsavel { get; }
    public Turma Turma { get; }
    public bool Ativa { get; }

    public static Result<Inscricao> Criar(string alunoCpf, Aluno aluno, Turma turma, string responsavel)
    {
        if (turma.Vagas <= 0)
            return Result.Failure<Inscricao>("Sem vagas disponíveis na turma.");

        if (aluno.Sexo == ESexo.Masculino && !turma.Masculino)
            return Result.Failure<Inscricao>("Turma não aceita alunos do sexo Masculino.");

        if (aluno.Sexo == ESexo.Feminino && !turma.Feminino)
            return Result.Failure<Inscricao>("Turma não aceita alunos do sexo Feminino.");

        if (aluno.Idade > turma.LimiteIdade)
            return Result.Failure<Inscricao>("Aluno acima do limite de idade da turma.");

        turma.DecrementarVagas();
        var inscricao = new Inscricao(Guid.NewGuid(), alunoCpf, responsavel, turma, true);
        return Result.Success(inscricao);
    }
}
