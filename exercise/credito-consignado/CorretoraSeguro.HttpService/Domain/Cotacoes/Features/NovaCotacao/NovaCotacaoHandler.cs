using CorretoraSeguro.HttpService.Controller;
using CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao.Workflow;
using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;
using WorkflowCore.Interface;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.NovaCotacao
{
    public class NovaCotacaoHandler
    {
        private readonly PropostasDbContext _dbContext;
        private readonly IWorkflowHost _workflowHost;

        public NovaCotacaoHandler(
            PropostasDbContext dbContext,
            IWorkflowHost workflowHost)
        {
            _dbContext = dbContext;
            _workflowHost = workflowHost;
        }

        public async Task<Result<Guid>> Handle(NovaCotacaoCommand command, CancellationToken cancellationToken)
        {
            var cotacaoResult = Cotacao.Criar(
                new Cotacao.DadosVeiculo(
                    command.Veiculo.Marca,
                    command.Veiculo.Modelo,
                    command.Veiculo.Ano),
                new  Cotacao.DadosProprietario(
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
                command.Coberturas);

            if (cotacaoResult.IsFailure)
                return Result.Failure<Guid>(cotacaoResult.Error);

            var cotacao = cotacaoResult.Value;
            _dbContext.Cotacoes.Add(cotacao);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _workflowHost.StartWorkflow(
                cotacaoResult.Value.Id.ToString(),
                new CotacaoData { CotacaoId = cotacao.Id });

            return Result.Success(cotacao.Id);
        }
    }
} 