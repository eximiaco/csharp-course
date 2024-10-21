using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Entidades;
using EscolaEximia.HttpService.Dominio.Infraestrutura;

namespace EscolaEximia.HttpService.Handlers;

public class RealizarInscricaoHandler
{
    private readonly InscricoesRepositorio _inscricoesRepositorio;

    public RealizarInscricaoHandler(InscricoesRepositorio inscricoesRepositorio)
    {
        _inscricoesRepositorio = inscricoesRepositorio;
    }

    public async Task<Result<Inscricao>> Handle(RealizarInscricaoCommand command, CancellationToken cancellationToken)
    {
        if (!await _inscricoesRepositorio.AlunoExiste(command.Aluno))
            return Result.Failure<Inscricao>("Aluno inválido");
        
        if (!await _inscricoesRepositorio.ResponsavelExiste(command.Responsavel))
            return Result.Failure<Inscricao>("Responsável inválido");
        
        var turmaResult = await _inscricoesRepositorio.RecuperarTurma(command.Turma, cancellationToken);
        if (turmaResult.HasNoValue)
            return Result.Failure<Inscricao>("Turma inválida");

        var turma = turmaResult.Value;

        if (turma.Vagas - 1 < 0)
            return Result.Failure<Inscricao>("Sem vagas");
        
        var alunoResult = await _inscricoesRepositorio.RecuperarAluno(command.Aluno);
        var aluno = alunoResult.Value;
        
        if (aluno.Sexo != ESexo.Feminino && turma.Masculino)
            return Result.Failure<Inscricao>("Turma válida apenas para sexo Masculino");
        
        if (aluno.Sexo != ESexo.Feminino && turma.Feminino)
            return Result.Failure<Inscricao>("Turma válida apenas para sexo Feminino");

        if (aluno.Idade > turma.LimiteIdade)
            return Result.Failure<Inscricao>("Fora do limite de idade da turma");
        
        var inscricao = new Inscricao
        {
            Aluno = command.Aluno,
            Responsavel = command.Responsavel,
            Turma = turma.Id,
            Ativa = true,
            Id = Guid.NewGuid()
        };
        
        await _inscricoesRepositorio.Adicionar(inscricao, cancellationToken);
        await _inscricoesRepositorio.Save();

        return Result.Success(inscricao);
    }
}
