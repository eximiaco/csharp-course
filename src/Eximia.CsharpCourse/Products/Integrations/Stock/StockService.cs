﻿using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Products.Integrations.Stock.Responses;
using Eximia.CsharpCourse.SeedWork;
using Eximia.CsharpCourse.SeedWork.Settings;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;

namespace Eximia.CsharpCourse.Products.Integrations.Stock;

public class StockService : IStockService
{
    private readonly StockApiSettings _settings;

    public StockService(IOptions<StockApiSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<Result<IEnumerable<StockResponse>>> CheckProductStock(IEnumerable<int> productIds, CancellationToken cancellationToken)
    {
        var response = await HttpRetryPolicy.AsyncRetryPolicy.ExecuteAndCaptureAsync(async () =>
        {
            return await _settings.Uri
                .WithHeader("x-api-key", _settings.ApiKey)
                .PostJsonAsync(productIds, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }).WaitAsync(cancellationToken).ConfigureAwait(false);

        if (response.Outcome == OutcomeType.Failure)
            return Result.Failure<IEnumerable<StockResponse>>("Falha consultando o estoque dos itens.");

        if (!response.Result.ResponseMessage.IsSuccessStatusCode)
            return Result.Failure<IEnumerable<StockResponse>>(await response.Result.ResponseMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));

        var result = await response.Result.GetJsonAsync<IEnumerable<StockResponse>>().WaitAsync(cancellationToken).ConfigureAwait(false);
        return Result.Success(result);
    }
}
