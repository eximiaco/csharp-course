using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes;
using EscolaEximia.HttpService.Dominio.Inscricoes.Aplicacao;
using EscolaEximia.HttpService.Dominio.Inscricoes.Infra;
using EscolaEximia.HttpService.Dominio.Regras;
using EscolaEximia.HttpService.Dominio.Regras.infra;
using Moq;

namespace EscolaEximia.TestesUnidade
{
    public class RealizarInscricaoHandlerTests
    {
        private readonly Mock<InscricoesRepositorio> _mockInscricoesRepositorio;
        private readonly Mock<RegraPorTurmaRepository> _mockRegraPorTurmaRepository;
        private readonly RealizarInscricaoHandler _handler;

        public RealizarInscricaoHandlerTests()
        {
            _mockInscricoesRepositorio = new Mock<InscricoesRepositorio>();
            _mockRegraPorTurmaRepository = new Mock<RegraPorTurmaRepository>();
            _handler = new RealizarInscricaoHandler(_mockInscricoesRepositorio.Object, _mockRegraPorTurmaRepository.Object);
        }

        [Fact]
        public async Task Handle_QuandoTodosOsDadosSaoValidos_DeveRetornarSucesso()
        {
            // Arrange
            var command = new RealizarInscricaoCommand("12345678900", "12345678900", 1);
            var aluno = new Aluno("12345678900", ESexo.Masculino, 15);
            var turma = new Turma(1, 10, true, true, 18);
            var regras = new List<RegraPorTurma>
            {
                new RegraPorTurma(Guid.NewGuid(), 1, new ValidacaoVagas()),
                new RegraPorTurma(Guid.NewGuid(), 1, new ValidacaoSexo()),
                new RegraPorTurma(Guid.NewGuid(), 1, new ValidacaoIdade())
            };

            _mockInscricoesRepositorio.Setup(r => r.ResponsavelExiste(It.IsAny<string>())).ReturnsAsync(true);
            _mockInscricoesRepositorio.Setup(r => r.RecuperarAluno(It.IsAny<string>())).ReturnsAsync(Maybe<Aluno>.From(aluno));
            _mockInscricoesRepositorio.Setup(r => r.RecuperarTurma(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Maybe<Turma>.From(turma));
            _mockRegraPorTurmaRepository.Setup(r => r.ObterRegrasPorTurmaAsync(It.IsAny<int>())).ReturnsAsync(regras);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _mockInscricoesRepositorio.Verify(r => r.Adicionar(It.IsAny<Inscricao>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockInscricoesRepositorio.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public async Task Handle_QuandoResponsavelNaoExiste_DeveRetornarFalha()
        {
            // Arrange
            var command = new RealizarInscricaoCommand("12345678900", "12345678900",1);
            _mockInscricoesRepositorio.Setup(r => r.ResponsavelExiste(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Responsável inválido", result.Error);
        }

        [Fact]
        public async Task Handle_QuandoAlunoNaoExiste_DeveRetornarFalha()
        {
            // Arrange
            var command = new RealizarInscricaoCommand("12345678900", "12345678900",1);
            _mockInscricoesRepositorio.Setup(r => r.ResponsavelExiste(It.IsAny<string>())).ReturnsAsync(true);
            _mockInscricoesRepositorio.Setup(r => r.RecuperarAluno(It.IsAny<string>())).ReturnsAsync(Maybe<Aluno>.None);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Aluno inválido", result.Error);
        }

        [Fact]
        public async Task Handle_QuandoTurmaNaoExiste_DeveRetornarFalha()
        {
            // Arrange
            var command = new RealizarInscricaoCommand("12345678900", "12345678900",1);
            var aluno = new Aluno("12345678900", ESexo.Masculino, 15);
            _mockInscricoesRepositorio.Setup(r => r.ResponsavelExiste(It.IsAny<string>())).ReturnsAsync(true);
            _mockInscricoesRepositorio.Setup(r => r.RecuperarAluno(It.IsAny<string>())).ReturnsAsync(Maybe<Aluno>.From(aluno));
            _mockInscricoesRepositorio.Setup(r => r.RecuperarTurma(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Maybe<Turma>.None);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Turma inválida", result.Error);
        }

        [Fact]
        public async Task Handle_QuandoValidacaoFalha_DeveRetornarFalha()
        {
            // Arrange
            var command = new RealizarInscricaoCommand("12345678900", "12345678900", 1);
            var aluno = new Aluno("12345678900", ESexo.Masculino, 20);
            var turma = new Turma(1, 10, true, true, 18);
            var regras = new List<RegraPorTurma>
            {
                new RegraPorTurma(Guid.NewGuid(), 1, new ValidacaoIdade())
            };

            _mockInscricoesRepositorio.Setup(r => r.ResponsavelExiste(It.IsAny<string>())).ReturnsAsync(true);
            _mockInscricoesRepositorio.Setup(r => r.RecuperarAluno(It.IsAny<string>())).ReturnsAsync(Maybe<Aluno>.From(aluno));
            _mockInscricoesRepositorio.Setup(r => r.RecuperarTurma(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Maybe<Turma>.From(turma));
            _mockRegraPorTurmaRepository.Setup(r => r.ObterRegrasPorTurmaAsync(It.IsAny<int>())).ReturnsAsync(regras);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Aluno acima do limite de idade da turma.", result.Error);
        }
    }
}