using CorretoraSeguro.HttpService.Domain.Coberturas;
using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.FluxoNovaCotacao;
using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;
using WorkflowCore.Interface;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao;

public class NovaCotacaoHandler
{
    private readonly CoberturasRepository _coberturasRepository;
    private readonly CotacoesRepository _cotacoesRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly IWorkflowHost _workflowHost;

    public NovaCotacaoHandler(
        CoberturasRepository coberturasRepository,
        CotacoesRepository cotacoesRepository,
        UnitOfWork unitOfWork,
        IWorkflowHost workflowHost)
    {
        _coberturasRepository = coberturasRepository;
        _cotacoesRepository = cotacoesRepository;
        _unitOfWork = unitOfWork;
        _workflowHost = workflowHost;
    }

    public async Task<Result<Guid>> Handle(NovaCotacaoCommand command, CancellationToken cancellationToken)
    {
        var coberturas = await _coberturasRepository.RecuperarPeloNomeAsync(command.Coberturas, cancellationToken).ConfigureAwait(false);
        if (!coberturas.Any())
            return Result.Failure<Guid>("Nenhuma cobertura válida encontrada.");

        var cotacaoResult = Cotacao.Criar(
            new Cotacao.DadosVeiculo(
                command.Veiculo.Marca,
                command.Veiculo.Modelo,
                command.Veiculo.Ano),
            new Cotacao.DadosProprietario(
                command.Proprietario.Cpf,
                command.Proprietario.Nome,
                command.Proprietario.DataNascimento,
                new Cotacao.DadosEndereco(
                    command.Proprietario.Residencia.Cep,
                    command.Proprietario.Residencia.Cidade,
                    command.Proprietario.Residencia.UF)),
            new Cotacao.DadosCondutor(
                command.Condutor.Cpf,
                command.Condutor.DataNascimento,
                new Cotacao.DadosEndereco(
                    command.Condutor.Residencia.Cep,
                    command.Condutor.Residencia.Cidade,
                    command.Condutor.Residencia.UF)),
            coberturas);

        if (cotacaoResult.IsFailure)
            return Result.Failure<Guid>(cotacaoResult.Error);

        var cotacao = cotacaoResult.Value;
        await _cotacoesRepository.AddAsync(cotacao, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

        await _workflowHost.StartWorkflow(
            cotacaoResult.Value.Id.ToString(),
            new CotacaoData { CotacaoId = cotacao.Id });

        return Result.Success(cotacao.Id);
    }
}
