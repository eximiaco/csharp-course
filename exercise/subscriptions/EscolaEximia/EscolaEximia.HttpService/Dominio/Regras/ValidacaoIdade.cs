using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes;

namespace EscolaEximia.HttpService.Dominio.Regras;

public class ValidacaoIdade : IValidacaoInscricao
{
    public Result Validar(Aluno aluno, Turma turma)
    {
        if (aluno.Idade > turma.LimiteIdade)
            return Result.Failure("Aluno acima do limite de idade da turma.");
        return Result.Success();
    }
}