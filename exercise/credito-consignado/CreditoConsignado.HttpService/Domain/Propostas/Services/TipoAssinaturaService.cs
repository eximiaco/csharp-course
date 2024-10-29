namespace CreditoConsignado.HttpService.Domain.Propostas.Services;

public sealed class TipoAssinaturaService
{
    public string ObterTipoAssinatura(Proponente proponente, List<string> UFsAssinaturaHibrida)
    {
        var ufTelefone = proponente.Contato.ObterUFTelefone();

        if (UFsAssinaturaHibrida.Any(c => c == ufTelefone)) return "Hibrida";
        return ufTelefone == proponente.Residencial.Estado 
            ? "Eletronica" 
            : "Figital";
    }
}
