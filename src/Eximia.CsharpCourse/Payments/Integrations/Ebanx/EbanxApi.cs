using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments.Integrations.Ebanx.Dtos;
using Eximia.CsharpCourse.SeedWork;
using Eximia.CsharpCourse.SeedWork.Settings;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;

namespace Eximia.CsharpCourse.Payments.Integrations.Ebanx;

public class EbanxApi : IEbanxApi
{
    private readonly EbanxApiSettings _settings;

    public EbanxApi(IOptions<EbanxApiSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<Result<string>> CaptureCreditCardTransactionAsync(Payment payment, CancellationToken cancellationToken)
    {
        var response = await HttpRetryPolicy.AsyncRetryPolicy.ExecuteAndCaptureAsync(async () =>
        {
            return await _settings.CreditCardUri
                .WithHeader("x-api-key", _settings.ApiKey)
                .PostJsonAsync(new { TransactionId = payment.OrderId }, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }).WaitAsync(cancellationToken).ConfigureAwait(false);

        if (response.Outcome == OutcomeType.Failure)
            return Result.Failure<string>("Falha realizando transação via cartão de crédito.");

        if (!response.Result.ResponseMessage.IsSuccessStatusCode)
            return Result.Failure<string>(await response.Result.ResponseMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));

        var result = await response.Result.GetJsonAsync<EbanxTransactionResponse>().WaitAsync(cancellationToken).ConfigureAwait(false);
        return result.Id;
    }

    public async Task<Result<string>> CapturePixTransactionAsync(Payment payment, CancellationToken cancellationToken)
    {
        var response = await HttpRetryPolicy.AsyncRetryPolicy.ExecuteAndCaptureAsync(async () =>
        {
            return await _settings.PixUri
                .WithHeader("x-api-key", _settings.ApiKey)
                .PostJsonAsync(new { TransactionId = payment.OrderId }, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }).WaitAsync(cancellationToken).ConfigureAwait(false);

        if (response.Outcome == OutcomeType.Failure)
            return Result.Failure<string>("Falha realizando transação via PIX.");

        if (!response.Result.ResponseMessage.IsSuccessStatusCode)
            return Result.Failure<string>(await response.Result.ResponseMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));

        var result = await response.Result.GetJsonAsync<EbanxTransactionResponse>().WaitAsync(cancellationToken).ConfigureAwait(false);
        return result.Id;
    }
}
