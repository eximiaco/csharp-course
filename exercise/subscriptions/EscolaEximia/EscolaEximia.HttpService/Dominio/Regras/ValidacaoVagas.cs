using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes;

namespace EscolaEximia.HttpService.Dominio.Regras;

public class ValidacaoVagas : IValidacaoInscricao
{
    public Result Validar(Aluno aluno, Turma turma)
    {
        if (turma.Vagas <= 0)
            return Result.Failure("Sem vagas disponíveis na turma.");
        return Result.Success();
    }
}