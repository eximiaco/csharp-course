using EscolaEximia.HttpService.Dominio.Inscricoes;
using Xunit;

namespace EscolaEximia.TestesUnidade;

public class InscricaoTestes
{
    [Fact]
    public void DeveCriarInscricaoEDecrementarVagasQuandoTodosOsCriteriosSaoAtendidos()
    {
        // Arrange
        var aluno = new Aluno("12345678900", ESexo.Masculino, 15);
        var turma = new Turma(id: 1, vagas: 10, masculino: true, feminino: true, limiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = Inscricao.Criar(aluno.Cpf, aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal(aluno.Cpf, resultado.Value.AlunoCpf);
        Assert.Equal(turma.Id, resultado.Value.Turma.Id);
        Assert.Equal(responsavelId, resultado.Value.Responsavel);
        Assert.True(resultado.Value.Ativa);
        Assert.Equal(9, turma.Vagas); // Verifica se as vagas foram decrementadas
    }

    [Fact]
    public void DeveRetornarFalhaQuandoNaoHaVagas()
    {
        // Arrange
        var aluno = new Aluno("12345678900", ESexo.Masculino, 15);
        var turma = new Turma(id: 1, vagas: 0, masculino: true, feminino: true, limiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = Inscricao.Criar(aluno.Cpf, aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsFailure);
        Assert.Equal("Sem vagas disponíveis na turma.", resultado.Error);
    }

    [Fact]
    public void DeveRetornarFalhaQuandoSexoMasculinoNaoPermitido()
    {
        // Arrange
        var aluno = new Aluno("12345678900", ESexo.Masculino, 15);
        var turma = new Turma(id: 1, vagas: 10, masculino: false, feminino: true, limiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = Inscricao.Criar(aluno.Cpf, aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsFailure);
        Assert.Equal("Turma não aceita alunos do sexo Masculino.", resultado.Error);
    }

    [Fact]
    public void DeveRetornarFalhaQuandoSexoFemininoNaoPermitido()
    {
        // Arrange
        var aluno = new Aluno("12345678900", ESexo.Feminino, 15);
        var turma = new Turma(id: 1, vagas: 10, masculino: true, feminino: false, limiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = Inscricao.Criar(aluno.Cpf, aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsFailure);
        Assert.Equal("Turma não aceita alunos do sexo Feminino.", resultado.Error);
    }

    [Fact]
    public void DeveRetornarFalhaQuandoIdadeAcimaDoLimite()
    {
        // Arrange
        var aluno = new Aluno("12345678900", ESexo.Masculino, 20);
        var turma = new Turma(id: 1, vagas: 10, masculino: true, feminino: true, limiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = Inscricao.Criar(aluno.Cpf, aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsFailure);
        Assert.Equal("Aluno acima do limite de idade da turma.", resultado.Error);
    }
}
