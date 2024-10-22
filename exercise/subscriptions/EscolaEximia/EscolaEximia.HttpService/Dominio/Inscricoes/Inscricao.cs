using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace EscolaEximia.HttpService.Dominio.Inscricoes;

public sealed class Inscricao : Entity<Guid>
{
    private Inscricao()
    {
    }

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
        var validacoes = new List<IValidacaoInscricao>
        {
            new ValidacaoVagas(),
            new ValidacaoSexo(),
            new ValidacaoIdade()
        };

        foreach (var validacao in validacoes)
        {
            var resultado = validacao.Validar(aluno, turma);
            if (resultado.IsFailure)
                return Result.Failure<Inscricao>(resultado.Error);
        }

        turma.DecrementarVagas();
        var inscricao = new Inscricao(Guid.NewGuid(), alunoCpf, responsavel, turma, true);
        return Result.Success(inscricao);
    }
}

public interface IValidacaoInscricao
{
    Result Validar(Aluno aluno, Turma turma);
}

public class ValidacaoVagas : IValidacaoInscricao
{
    public Result Validar(Aluno aluno, Turma turma)
    {
        if (turma.Vagas <= 0)
            return Result.Failure("Sem vagas disponíveis na turma.");
        return Result.Success();
    }
}

public class ValidacaoSexo : IValidacaoInscricao
{
    public Result Validar(Aluno aluno, Turma turma)
    {
        if (aluno.Sexo == ESexo.Masculino && !turma.Masculino)
            return Result.Failure("Turma não aceita alunos do sexo Masculino.");
        if (aluno.Sexo == ESexo.Feminino && !turma.Feminino)
            return Result.Failure("Turma não aceita alunos do sexo Feminino.");
        return Result.Success();
    }
}

public class ValidacaoIdade : IValidacaoInscricao
{
    public Result Validar(Aluno aluno, Turma turma)
    {
        if (aluno.Idade > turma.LimiteIdade)
            return Result.Failure("Aluno acima do limite de idade da turma.");
        return Result.Success();
    }
}
