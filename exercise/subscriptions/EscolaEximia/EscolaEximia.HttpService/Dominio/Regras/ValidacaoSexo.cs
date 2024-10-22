using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes;

namespace EscolaEximia.HttpService.Dominio.Regras;

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