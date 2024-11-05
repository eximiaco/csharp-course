using CreditoConsignado.HttpService.Domain.Propostas;
using CreditoConsignado.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

public sealed class AdicionarTagHandler
{
    private readonly PropostaRepository _propostaRepository;
    private readonly TagRepository _tagRepository;
    private readonly UnitOfWork _unitOfWork;

    public AdicionarTagHandler(
        PropostaRepository propostaRepository,
        TagRepository tagRepository,
        UnitOfWork unitOfWork)
    {
        _propostaRepository = propostaRepository;
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecutarAsync(AdicionarTagCommand command, CancellationToken cancellationToken)
    {
        var propostaMaybe = await _propostaRepository.Obter(command.PropostaId, cancellationToken);
        if (propostaMaybe.HasNoValue)
            return Result.Failure("Proposta não encontrada");

        var tagMaybe = await _tagRepository.Obter(command.TagId, cancellationToken);
        if (tagMaybe.HasNoValue)
            return Result.Failure("Tag não encontrada");

        var proposta = propostaMaybe.Value;
        var resultado = proposta.AdicionarTag(tagMaybe.Value);
        
        if (resultado.IsFailure)
            return Result.Failure(resultado.Error);

        await _unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }
}