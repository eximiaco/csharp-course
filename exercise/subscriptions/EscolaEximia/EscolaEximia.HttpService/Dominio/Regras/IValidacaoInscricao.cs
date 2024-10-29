using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes;

namespace EscolaEximia.HttpService.Dominio.Regras;

public interface IValidacaoInscricao
{
    Result Validar(Aluno aluno, Turma turma);
}