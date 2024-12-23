using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes.Infra;
using EscolaEximia.HttpService.Dominio.Regras;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EscolaEximia.HttpService.Dominio.Regras.infra;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Aplicacao;

public class RealizarInscricaoHandler(
    InscricoesRepositorio inscricoesRepositorio,
    RegraPorTurmaRepository regraPorTurmaRepository)
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

        var regrasPorTurma = await regraPorTurmaRepository.ObterRegrasPorTurmaAsync(turmaResult.Value.Id);

        var inscricaoResult = Inscricao.Criar(
            alunoResult.Value, 
            turmaResult.Value, 
            command.Responsavel, 
            regrasPorTurma.Select(c=> c.Regra));
        
        if (inscricaoResult.IsFailure)
            return Result.Failure<Inscricao>(inscricaoResult.Error);

        var inscricao = inscricaoResult.Value;
        await inscricoesRepositorio.Adicionar(inscricao, cancellationToken);
        await inscricoesRepositorio.Save();

        return Result.Success(inscricao);
    }
}
