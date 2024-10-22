using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Entidades;
using EscolaEximia.HttpService.Dominio.Infraestrutura;
using EscolaEximia.HttpService.Dominio.Factories;

namespace EscolaEximia.HttpService.Handlers;

public class RealizarInscricaoHandler(InscricoesRepositorio inscricoesRepositorio, InscricaoFactory inscricaoFactory)
{
    public async Task<Result<Inscricao>> Handle(RealizarInscricaoCommand command, CancellationToken cancellationToken)
    {
        if (!await inscricoesRepositorio.ResponsavelExiste(command.Responsavel))
            return Result.Failure<Inscricao>("Responsável inválido");
        
        var alunoResult = await inscricoesRepositorio.RecuperarAluno(command.Aluno);
        if (alunoResult.HasNoValue)
            return Result.Failure<Inscricao>("Aluno inválido");

        var turmaResult = await inscricoesRepositorio.RecuperarTurma(command.Turma, cancellationToken);
        if (turmaResult.HasNoValue)
            return Result.Failure<Inscricao>("Turma inválida");

        var inscricaoResult = inscricaoFactory.Criar(alunoResult.Value, turmaResult.Value, command.Responsavel);
        
        if (inscricaoResult.IsFailure)
            return Result.Failure<Inscricao>(inscricaoResult.Error);

        var inscricao = inscricaoResult.Value;
        await inscricoesRepositorio.Adicionar(inscricao, cancellationToken);
        await inscricoesRepositorio.Save();

        return Result.Success(inscricao);
    }
}
