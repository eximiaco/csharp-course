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

        private readonly List<Cobertura> _coberturas = new();
        public IReadOnlyCollection<Cobertura> Coberturas => _coberturas.AsReadOnly();

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
            DadosCondutor condutor)
        {
            Id = Guid.NewGuid();
            Veiculo = veiculo;
            Proprietario = proprietario;
            Condutor = condutor;
            Status = StatusCotacao.Nova;
            DataCriacao = DateTime.UtcNow;
        }

        public static Result<Cotacao> Criar(
            DadosVeiculo veiculo,
            DadosProprietario proprietario,
            DadosCondutor condutor,
            List<string> coberturasSolicitadas)
        {
            if (veiculo is null)
                return Result.Failure<Cotacao>("Veículo é obrigatório");

            if (proprietario is null)
                return Result.Failure<Cotacao>("Proprietário é obrigatório");

            if (condutor is null)
                return Result.Failure<Cotacao>("Condutor é obrigatório");

            if (coberturasSolicitadas is null || !coberturasSolicitadas.Any())
                return Result.Failure<Cotacao>("Pelo menos uma cobertura deve ser informada");

            var cotacao = new Cotacao(
                veiculo,
                proprietario,
                condutor);

            foreach (var coberturaSolicitada in coberturasSolicitadas)
            {
                if (!Enum.TryParse<TipoCobertura>(coberturaSolicitada, true, out var tipoCobertura))
                    return Result.Failure<Cotacao>($"Tipo de cobertura inválida: {coberturaSolicitada}");

                cotacao.AdicionarCobertura(tipoCobertura, 0); // Valor inicial 0, será calculado posteriormente
            }

            return Result.Success(cotacao);
        }

        public void AtualizarRisco(int nivelRisco)
        {
            NivelRisco = nivelRisco;
            Status = StatusCotacao.RiscoCalculado;
        }

        public void AtualizarValorBase(decimal valorBase)
        {
            ValorBase = valorBase;
            Status = StatusCotacao.ValorBaseCalculado;
        }

        public void AtualizarValorFinal(decimal valorFinal)
        {
            ValorFinal = valorFinal;
            Status = StatusCotacao.AguardandoAprovacao;
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

        public void AdicionarCobertura(TipoCobertura tipo, decimal valorMercado)
        {
            var valorCobertura = CalcularValorCobertura(tipo, valorMercado);
            
            var coberturaExistente = _coberturas.FirstOrDefault(c => c.Tipo == tipo);
            if (coberturaExistente != null)
            {
                coberturaExistente.AtualizarValor(valorCobertura);
                return;
            }

            var cobertura = new Cobertura(tipo, valorCobertura);
            _coberturas.Add(cobertura);
        }

        private decimal CalcularValorCobertura(TipoCobertura tipo, decimal valorMercado)
        {
            return tipo switch
            {
                TipoCobertura.Basica => valorMercado * 0.03m,
                TipoCobertura.Roubo => valorMercado * 0.05m,
                TipoCobertura.Vidros => valorMercado * 0.02m,
                _ => throw new ArgumentException("Tipo de cobertura inválido", nameof(tipo))
            };
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