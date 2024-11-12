using CorretoraSeguro.HttpService.Domain.Cotacoes;

public interface IEmailService
{
    Task EnviarEmailCotacao(Cotacao cotacao);
} 