using EscolaEximia.HttpService.Dominio.Entidades;
using EscolaEximia.HttpService.Dominio.Factories;
using Xunit;

namespace EscolaEximia.TestesUnidade;

public class InscricaoFactoryTestes
{
    private readonly InscricaoFactory _factory;

    public InscricaoFactoryTestes()
    {
        _factory = new InscricaoFactory();
    }

    [Fact]
    public void DeveCriarInscricaoQuandoTodosOsCriteriosSaoAtendidos()
    {
        // Arrange
        var aluno = new Aluno { Cpf = "12345678900", Sexo = ESexo.Masculino, Idade = 15 };
        var turma = new Turma(Id: 1, Vagas: 10, Masculino: true, Feminino: true, LimiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = _factory.Criar(aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal(aluno.Cpf, resultado.Value.Aluno);
        Assert.Equal(turma.Id, resultado.Value.Turma);
        Assert.Equal(responsavelId, resultado.Value.Responsavel);
        Assert.True(resultado.Value.Ativa);
    }

    [Fact]
    public void DeveRetornarFalhaQuandoNaoHaVagas()
    {
        // Arrange
        var aluno = new Aluno { Cpf = "12345678900", Sexo = ESexo.Masculino, Idade = 15 };
        var turma = new Turma(Id: 1, Vagas: 0, Masculino: true, Feminino: true, LimiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = _factory.Criar(aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsFailure);
        Assert.Equal("Sem vagas", resultado.Error);
    }

    [Fact]
    public void DeveRetornarFalhaQuandoSexoMasculinoNaoPermitido()
    {
        // Arrange
        var aluno = new Aluno { Cpf = "12345678900", Sexo = ESexo.Masculino, Idade = 15 };
        var turma = new Turma(Id: 1, Vagas: 10, Masculino: false, Feminino: true, LimiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = _factory.Criar(aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsFailure);
        Assert.Equal("Turma não aceita alunos do sexo Masculino", resultado.Error);
    }

    [Fact]
    public void DeveRetornarFalhaQuandoSexoFemininoNaoPermitido()
    {
        // Arrange
        var aluno = new Aluno { Cpf = "12345678900", Sexo = ESexo.Feminino, Idade = 15 };
        var turma = new Turma(Id: 1, Vagas: 10, Masculino: true, Feminino: false, LimiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = _factory.Criar(aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsFailure);
        Assert.Equal("Turma não aceita alunos do sexo Feminino", resultado.Error);
    }

    [Fact]
    public void DeveRetornarFalhaQuandoIdadeAcimaDoLimite()
    {
        // Arrange
        var aluno = new Aluno { Cpf = "12345678900", Sexo = ESexo.Masculino, Idade = 20 };
        var turma = new Turma(Id: 1, Vagas: 10, Masculino: true, Feminino: true, LimiteIdade: 18);
        var responsavelId = "98765432100";

        // Act
        var resultado = _factory.Criar(aluno, turma, responsavelId);

        // Assert
        Assert.True(resultado.IsFailure);
        Assert.Equal("Fora do limite de idade da turma", resultado.Error);
    }
}
