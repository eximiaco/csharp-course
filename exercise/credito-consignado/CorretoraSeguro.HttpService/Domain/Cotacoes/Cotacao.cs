using CorretoraSeguro.HttpService.Controller;
using CSharpFunctionalExtensions;

namespace CorretoraSeguro.HttpService.Domain.Cotacoes
{
    public sealed class Cotacao : Entity<Guid>
    {
        public StatusCotacao Status { get; private set; }
        public DadosVeiculo Veiculo { get; private set; }
        public DadosProprietario Proprietario { get; private set; }
        public DadosCondutor Condutor { get; private set; }
        public List<string> Coberturas { get; private set; }
        public int? NivelRisco { get; private set; }
        public decimal? ValorBase { get; private set; }
        public decimal? ValorFinal { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAprovacao { get; private set; }

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
            List<string> coberturas)
        {
            Id = Guid.NewGuid();
            Veiculo = veiculo;
            Proprietario = proprietario;
            Condutor = condutor;
            Coberturas = coberturas;
            Status = StatusCotacao.Nova;
            DataCriacao = DateTime.UtcNow;
        }

        public static Result<Cotacao> Criar(
            DadosVeiculo veiculo,
            DadosProprietario proprietario,
            DadosCondutor condutor,
            List<string> coberturas)
        {
            if (veiculo is null)
                return Result.Failure<Cotacao>("Veículo é obrigatório");

            if (proprietario is null)
                return Result.Failure<Cotacao>("Proprietário é obrigatório");

            if (condutor is null)
                return Result.Failure<Cotacao>("Condutor é obrigatório");

            if (coberturas is null || !coberturas.Any())
                return Result.Failure<Cotacao>("Pelo menos uma cobertura deve ser selecionada");

            if (!CoberturasValidas(coberturas))
                return Result.Failure<Cotacao>("Uma ou mais coberturas selecionadas são inválidas");

            var cotacao = new Cotacao(
                veiculo,
                proprietario,
                condutor,
                coberturas);

            return Result.Success(cotacao);
        }

        private static bool CoberturasValidas(List<string> coberturas)
        {
            var coberturasPermitidas = new[] { "ROUBO", "COLISAO", "TERCEIROS", "RESIDENCIAL" };
            return coberturas.All(c => coberturasPermitidas.Contains(c));
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