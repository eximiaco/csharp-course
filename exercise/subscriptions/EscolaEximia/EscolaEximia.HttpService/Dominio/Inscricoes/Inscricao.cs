using CSharpFunctionalExtensions;
using System.Collections.Generic;
using EscolaEximia.HttpService.Dominio.Regras;

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

    public static Result<Inscricao> Criar(Aluno aluno, Turma turma, string responsavel, IEnumerable<IValidacaoInscricao> regras)
    {
        foreach (var regra in regras)
        {
            var resultado = regra.Validar(aluno, turma);
            if (resultado.IsFailure)
                return Result.Failure<Inscricao>(resultado.Error);
        }

        turma.DecrementarVagas();
        var inscricao = new Inscricao(Guid.NewGuid(), aluno.Cpf, responsavel, turma, true);
        return Result.Success(inscricao);
    }
}
