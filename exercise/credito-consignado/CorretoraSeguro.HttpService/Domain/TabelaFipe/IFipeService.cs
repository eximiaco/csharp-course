namespace CorretoraSeguro.HttpService.Domain.TabelaFipe;

public interface IFipeService
{
    Task<decimal> ObterValorAsync(string marca, string modelo, int anoModelo);
}