using CorretoraSeguro.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes
{
    public sealed class Cotacao : Entity<Guid>
    {
        public StatusCotacao Status { get; private set; }
        public DadosVeiculo Veiculo { get; private set; }
        public DadosProprietario Proprietario { get; private set; }
        public DadosCondutor Condutor { get; private set; }
        public int? NivelRisco { get; private set; }
        public decimal? ValorBase { get; private set; }
        public decimal? ValorFinal { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAprovacao { get; private set; }

        private readonly List<CoberturaCalculada> _coberturas = new();
        public IReadOnlyCollection<CoberturaCalculada> Coberturas => _coberturas.AsReadOnly();

        public record DadosVeiculo(
            string Marca,
            string Modelo,
            int Ano
        );

        public record DadosProprietario(
            string Cpf,
            string Nome,
            DateTime DataNascimento,
            DadosEndereco Residencia
        );

        public record DadosCondutor(
            string Cpf,
            DateTime DataNascimento,
            DadosEndereco Residencia
        );

        public record DadosEndereco(
            string Cep,
            string Cidade,
            string UF
        );

        private Cotacao(
            DadosVeiculo veiculo,
            DadosProprietario proprietario,
            DadosCondutor condutor,
            List<CoberturaCalculada> coberturas)
        {
            Id = Guid.NewGuid();
            Veiculo = veiculo;
            Proprietario = proprietario;
            Condutor = condutor;
            Status = StatusCotacao.Nova;
            DataCriacao = DateTime.UtcNow;
            _coberturas = coberturas;
        }

        public static Result<Cotacao> Criar(
            DadosVeiculo veiculo,
            DadosProprietario proprietario,
            DadosCondutor condutor,
            IEnumerable<Cobertura> coberturas)
        {
            var coberturasCalculadas = coberturas.Select(c => 
                new CoberturaCalculada(c.Tipo, 0) // Iniciando com valor zero
            ).ToList();

            if (veiculo is null)
                return Result.Failure<Cotacao>("Veículo é obrigatório");

            if (proprietario is null)
                return Result.Failure<Cotacao>("Proprietário é obrigatório");

            if (condutor is null)
                return Result.Failure<Cotacao>("Condutor é obrigatório");

            if (coberturasCalculadas is null || !coberturasCalculadas.Any())
                return Result.Failure<Cotacao>("Pelo menos uma cobertura deve ser informada");

            var cotacao = new Cotacao(
                veiculo,
                proprietario,
                condutor,
                coberturasCalculadas);

            return Result.Success(cotacao);
        }

        public void AtualizarRisco(int nivelRisco)
        {
            NivelRisco = nivelRisco;
            Status = StatusCotacao.RiscoCalculado;
        }

        public void AtualizarValorBase(decimal valorMercado)
        {
            ValorBase = Coberturas.Sum(cobertura => cobertura.CalcularValor(valorMercado));
        }

        public void Aprovar()
        {
            Status = StatusCotacao.Aprovada;
            DataAprovacao = DateTime.UtcNow;
        }

        public void Cancelar()
        {
            Status = StatusCotacao.Cancelada;
        }

        public void CalcularValorFinal()
        {
            if (!NivelRisco.HasValue || !ValorBase.HasValue)
                return;

            decimal fatorRisco = NivelRisco.Value switch
            {
                1 => 1.00m,
                2 => 1.05m,
                3 => 1.10m,
                4 => 1.20m,
                5 => 1.30m,
                _ => 1.00m
            };

            ValorFinal = ValorBase.Value * fatorRisco;
            Status = StatusCotacao.ValorBaseCalculado;
        }
    }

    public enum StatusCotacao
    {
        Nova,
        RiscoCalculado,
        ValorBaseCalculado,
        AguardandoAprovacao,
        Aprovada,
        Cancelada
    }
} 