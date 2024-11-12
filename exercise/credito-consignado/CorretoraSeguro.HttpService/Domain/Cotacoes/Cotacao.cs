using CSharpFunctionalExtensions;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes
{
    public sealed class Cotacao : Entity<Guid>
    {
        private readonly List<CoberturaCalculada> _coberturas = new();

        private Cotacao(
            Guid id,
            DadosVeiculo veiculo,
            DadosProprietario proprietario,
            DadosCondutor condutor,
            List<CoberturaCalculada> coberturas) : base(id)
        {
            Id = Guid.NewGuid();
            Veiculo = veiculo;
            Proprietario = proprietario;
            Condutor = condutor;
            Status = EStatusCotacao.Nova;
            DataCriacao = DateTime.UtcNow;
            _coberturas = coberturas;
        }

        public EStatusCotacao Status { get; private set; }
        public DadosVeiculo Veiculo { get; private set; }
        public DadosProprietario Proprietario { get; private set; }
        public DadosCondutor Condutor { get; private set; }
        public int? NivelRisco { get; private set; }
        public decimal? ValorBase { get; private set; }
        public decimal? ValorFinal { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAprovacao { get; private set; }
        public IReadOnlyCollection<CoberturaCalculada> Coberturas => _coberturas.AsReadOnly();

        public static Result<Cotacao> Criar(
            DadosVeiculo veiculo,
            DadosProprietario proprietario,
            DadosCondutor condutor,
            IEnumerable<Cobertura> coberturas)
        {
            var coberturasCalculadas = coberturas.Select(c => 
                new CoberturaCalculada(Guid.NewGuid(), c.Tipo, 0) // Iniciando com valor zero
            ).ToList();

            if (veiculo is null)
                return Result.Failure<Cotacao>("Veículo é obrigatório.");

            if (proprietario is null)
                return Result.Failure<Cotacao>("Proprietário é obrigatório.");

            if (condutor is null)
                return Result.Failure<Cotacao>("Condutor é obrigatório.");

            if (coberturasCalculadas is null || !coberturasCalculadas.Any())
                return Result.Failure<Cotacao>("Pelo menos uma cobertura deve ser informada.");

            var cotacao = new Cotacao(
                Guid.NewGuid(),
                veiculo,
                proprietario,
                condutor,
                coberturasCalculadas);

            return cotacao;
        }

        public void AtualizarRisco(int nivelRisco)
        {
            NivelRisco = nivelRisco;
            Status = EStatusCotacao.RiscoCalculado;
        }

        public void AtualizarValorBase(decimal valorMercado)
        {
            ValorBase = Coberturas.Sum(cobertura => cobertura.CalcularValor(valorMercado));
        }

        public void AtualizarValorFinal(decimal valorFinal)
        {
            ValorFinal = valorFinal;
            Status = EStatusCotacao.AguardandoAprovacao;
        }

        public Result Aprovar()
        {
            if (Status != EStatusCotacao.AguardandoAprovacao)
                return Result.Failure("Cotação não está aguardando aprovação.");

            Status = EStatusCotacao.Aprovada;
            DataAprovacao = DateTime.UtcNow;
            return Result.Success();
        }

        public void Cancelar()
        {
            Status = EStatusCotacao.Cancelada;
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
            Status = EStatusCotacao.ValorBaseCalculado;
        }

        public record DadosVeiculo(string Marca, string Modelo, int Ano);
        public record DadosProprietario(string Cpf, string Nome, DateTime DataNascimento, DadosEndereco Residencia);
        public record DadosCondutor(string Cpf, DateTime DataNascimento, DadosEndereco Residencia);
        public record DadosEndereco(string Cep, string Cidade, string UF);
    }

    public enum EStatusCotacao
    {
        Nova,
        RiscoCalculado,
        ValorBaseCalculado,
        AguardandoAprovacao,
        Aprovada,
        Cancelada
    }
} 