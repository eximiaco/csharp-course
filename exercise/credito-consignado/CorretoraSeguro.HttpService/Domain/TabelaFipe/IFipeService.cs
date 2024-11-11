namespace CorretoraSeguro.HttpService.Domain.TabelaFipe;

public interface IFipeService
{
    Task<object> ObterValor(string veiculoMarca, string veiculoModelo, int veiculoAno);
}