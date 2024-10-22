using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes.Infra;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Aplicacao;

public class RealizarInscricaoHandler
{
    private readonly InscricoesRepositorio _inscricoesRepositorio;

    public RealizarInscricaoHandler(InscricoesRepositorio inscricoesRepositorio)
    {
        _inscricoesRepositorio = inscricoesRepositorio;
    }

    public async Task<Result<Inscricao>> Handle(RealizarInscricaoCommand command, CancellationToken cancellationToken)
    {
        if (!await _inscricoesRepositorio.ResponsavelExiste(command.Responsavel))
            return Result.Failure<Inscricao>("Responsável inválido");
        
        var alunoResult = await _inscricoesRepositorio.RecuperarAluno(command.Aluno);
        if (alunoResult.HasNoValue)
            return Result.Failure<Inscricao>("Aluno inválido");

        var turmaResult = await _inscricoesRepositorio.RecuperarTurma(command.Turma, cancellationToken);
        if (turmaResult.HasNoValue)
            return Result.Failure<Inscricao>("Turma inválida");

        var inscricaoResult = Inscricao.Criar(command.Aluno, alunoResult.Value, turmaResult.Value, command.Responsavel);
        
        if (inscricaoResult.IsFailure)
            return Result.Failure<Inscricao>(inscricaoResult.Error);

        var inscricao = inscricaoResult.Value;
        await _inscricoesRepositorio.Adicionar(inscricao, cancellationToken);
        await _inscricoesRepositorio.Save();

        return Result.Success(inscricao);
    }
}
