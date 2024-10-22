using EscolaEximia.HttpService.Dominio.Inscricoes;
using Xunit;

namespace EscolaEximia.Tests
{
    public class ValidacoesInscricaoTests
    {
        [Fact]
        public void ValidacaoVagas_DeveRetornarSucesso_QuandoHouverVagas()
        {
            // Arrange
            var validacao = new ValidacaoVagas();
            var turma = new Turma(1, 5, true, true, 18);

            // Act
            var resultado = validacao.Validar(null, turma);

            // Assert
            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public void ValidacaoVagas_DeveRetornarFalha_QuandoNaoHouverVagas()
        {
            // Arrange
            var validacao = new ValidacaoVagas();
            var turma = new Turma(2, 0, true, true, 18);

            // Act
            var resultado = validacao.Validar(null, turma);

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Sem vagas disponíveis na turma.", resultado.Error);
        }

        [Fact]
        public void ValidacaoSexo_DeveRetornarSucesso_QuandoSexoPermitido()
        {
            // Arrange
            var validacao = new ValidacaoSexo();
            var aluno = new Aluno("12345678900", ESexo.Masculino, 15);
            var turma = new Turma(3, 10, true, true, 18);

            // Act
            var resultado = validacao.Validar(aluno, turma);

            // Assert
            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public void ValidacaoSexo_DeveRetornarFalha_QuandoSexoNaoPermitido()
        {
            // Arrange
            var validacao = new ValidacaoSexo();
            var aluno = new Aluno("12345678900", ESexo.Masculino, 15);
            var turma = new Turma(4, 10, false, true, 18);

            // Act
            var resultado = validacao.Validar(aluno, turma);

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Turma não aceita alunos do sexo Masculino.", resultado.Error);
        }

        [Fact]
        public void ValidacaoIdade_DeveRetornarSucesso_QuandoIdadeDentroDoLimite()
        {
            // Arrange
            var validacao = new ValidacaoIdade();
            var aluno = new Aluno("12345678900", ESexo.Masculino, 15);
            var turma = new Turma(5, 10, true, true, 18);

            // Act
            var resultado = validacao.Validar(aluno, turma);

            // Assert
            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public void ValidacaoIdade_DeveRetornarFalha_QuandoIdadeAcimaDoLimite()
        {
            // Arrange
            var validacao = new ValidacaoIdade();
            var aluno = new Aluno("12345678900", ESexo.Masculino, 20);
            var turma = new Turma(6, 10, true, true, 18);

            // Act
            var resultado = validacao.Validar(aluno, turma);

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Aluno acima do limite de idade da turma.", resultado.Error);
        }
    }
}
