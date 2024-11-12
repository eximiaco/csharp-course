namespace CorretoraSeguro.HttpService.Domain.Cotacoes.Features.CalcularValorFinal
{
    public class CalcularValorFinalCommand
    {
        public Guid CotacaoId { get; }

        public CalcularValorFinalCommand(Guid cotacaoId)
        {
            CotacaoId = cotacaoId;
        }
    }
} 