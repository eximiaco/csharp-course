using CSharpFunctionalExtensions;

namespace CreditoConsignado.HttpService.Domain.Propostas;

public interface ISituacaoProposta
{
    public abstract string Nome { get; }
    public abstract Result AdicionarPendencia();
    public abstract Result Aprovar();
    public abstract Result Reprovar();
}

public class EmAnalise(Proposta proposta) : ISituacaoProposta
{
    public string Nome => "EmAnalise";

    public Result AdicionarPendencia()
    {
        proposta.AlterarSituacao(new ComPendencias(proposta));
        return Result.Success();
    }

    public Result Aprovar()
    {
        proposta.AlterarSituacao(new Aprovada(proposta));
        return Result.Success();
    }

    public Result Reprovar()
    {
        proposta.AlterarSituacao(new Reprovada(proposta));
        return Result.Success();
    }
}

public class ComPendencias(Proposta proposta) : ISituacaoProposta
{
    public string Nome => "ComPendencias";

    public Result AdicionarPendencia()
        => Result.Success();

    public Result Aprovar()
    {
        proposta.AlterarSituacao(new Aprovada(proposta));
        return Result.Success();
    }

    public Result Reprovar()
    {
        proposta.AlterarSituacao(new Reprovada(proposta));
        return Result.Success();
    }
}

public class Aprovada(Proposta proposta) : ISituacaoProposta
{
    public string Nome => "Aprovada";

    public Result AdicionarPendencia()
        => Result.Failure("Não é possível adicionar pendências em uma proposta aprovada");

    public Result Aprovar()
        => Result.Failure("Proposta já está aprovada");

    public Result Reprovar()
        => Result.Failure("Não é possível reprovar uma proposta aprovada");
}

public class Reprovada(Proposta proposta) : ISituacaoProposta
{
    public string Nome => "Reprovada";

    public Result AdicionarPendencia()
        => Result.Failure("Não é possível adicionar pendências em uma proposta reprovada");

    public Result Aprovar()
        => Result.Failure("Não é possível aprovar uma proposta reprovada");

    public Result Reprovar()
        => Result.Failure("Proposta já está reprovada");
}