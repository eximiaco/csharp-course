using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Entidades;

namespace EscolaEximia.HttpService.Dominio.Factories;

public sealed class InscricaoFactory
{
    public Result<Inscricao> Criar(Aluno aluno, Turma turma, string responsavelId)
    {
        if (turma.Vagas - 1 < 0)
            return Result.Failure<Inscricao>("Sem vagas");
        
        if (aluno.Sexo == ESexo.Masculino && !turma.Masculino)
            return Result.Failure<Inscricao>("Turma não aceita alunos do sexo Masculino");
        
        if (aluno.Sexo == ESexo.Feminino && !turma.Feminino)
            return Result.Failure<Inscricao>("Turma não aceita alunos do sexo Feminino");

        if (aluno.Idade > turma.LimiteIdade)
            return Result.Failure<Inscricao>("Fora do limite de idade da turma");
        
        var inscricao = new Inscricao
        {
            Aluno = aluno.Cpf,
            Responsavel = responsavelId,
            Turma = turma.Id,
            Ativa = true,
            Id = Guid.NewGuid()
        };
        
        return Result.Success(inscricao);
    }
}
