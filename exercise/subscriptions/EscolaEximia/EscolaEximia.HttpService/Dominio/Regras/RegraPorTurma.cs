using System;
using CSharpFunctionalExtensions;

namespace EscolaEximia.HttpService.Dominio.Regras;

public class RegraPorTurma : Entity<Guid>
{
    // Construtor privado sem parâmetros para o EF
    private RegraPorTurma() { }

    public RegraPorTurma(Guid id, int turmaId, IValidacaoInscricao regra)
    {
        Id = id;
        TurmaId = turmaId;
        Regra = regra;
    }
    
    public int TurmaId { get; }
    public IValidacaoInscricao Regra { get; }
}
