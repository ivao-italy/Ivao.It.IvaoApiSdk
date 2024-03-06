using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Ivao.It.IvaoApiSdk.Accessors;
internal class BaseAccessor(ILogger logger)
{
    protected async Task RunApiCall(Func<Task<HttpResponseMessage>> callDelegate, string route)
    {
        logger.LogDebug("Calling {route}", route);

        var response = await callDelegate.Invoke();
        response.EnsureSuccessOrWrap(logger);

        logger.LogInformation("Call to {route} ended with status {status}", route, response.StatusCode);
    }


    protected async Task<T?> RunApiCall<T>(
        Func<Task<HttpResponseMessage>> callDelegate,
        string route,
        CancellationToken cancellation = default)
    {
        logger.LogDebug("Calling {route}", route);

        var response = await callDelegate.Invoke();
        await response.EnsureSuccessOrWrap(logger);

        var data = await response.Content.ReadFromJsonAsync<T>(cancellation);

        logger.LogInformation("Call to {route} ended with status {status}", route, response.StatusCode);
        return data;
    }
}
