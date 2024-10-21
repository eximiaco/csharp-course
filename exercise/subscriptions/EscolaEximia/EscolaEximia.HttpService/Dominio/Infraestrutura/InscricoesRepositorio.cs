using CSharpFunctionalExtensions;
using Dapper;
using EscolaEximia.HttpService.Comum;
using EscolaEximia.HttpService.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EscolaEximia.HttpService.Dominio.Infraestrutura;

public sealed class InscricoesRepositorio(
    InscricoesDbContext dbContext,
    Serilog.ILogger logger) : IService<InscricoesRepositorio>
{
    public async Task<bool> AlunoExiste(string aluno)
    {
        var result = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>("SELECT codigo FROM Inscricoes.Alunos WHERE codigo = @aluno",
            new {aluno});
        
        if(result != aluno)
            logger.Warning("Aluno não foi localizado no banco de dados");
        
        return result == aluno;
    }
    
    public async Task<bool> ResponsavelExiste(string responsavel)
    {
        var result = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>("SELECT codigo FROM Inscricoes.Responsaveis WHERE codigo = @responsavel",
                new {responsavel});
        return result == responsavel;
    }

    public async Task<Maybe<Aluno>> RecuperarAluno(string cpf)
    {
        return (await dbContext.Alunos.FirstOrDefaultAsync(c => c.Cpf == cpf)) ?? Maybe<Aluno>.None;
    }
    
    public async Task<Maybe<Turma>> RecuperarTurma(int id, CancellationToken cancellationToken)
    {
        var turma = await dbContext.Turmas.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return turma ?? Maybe<Turma>.None;
    }

    public async Task Adicionar(Inscricao inscricao, CancellationToken cancellationToken)
    {
        await dbContext.Inscricoes.AddAsync(inscricao, cancellationToken);
    }

    public Task Save()
    {
        return dbContext.SaveChangesAsync();
    }   
}