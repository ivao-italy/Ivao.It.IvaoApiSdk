using Ivao.It.ApiSdk.Auth;

using Microsoft.Extensions.Logging;

namespace Ivao.It.ApiSdk.Accessors;

internal class TrackerApi(
    IAuthenticator authenticator,
    ILogger<TrackerApi> logger,
    HttpClient client) : ITrackerApi
{
    public async Task GetAtcSummary(CancellationToken cancellation = default)
    {
        const string route = @"v2/tracker/now/atc/summary";

        logger.LogDebug("Calling {route}", route);

        var response = await client
            .AddToken(await authenticator.GetToken(cancellation))
            .GetAsync(route, cancellation);

        //Todo deserialize
        var data = await response.Content.ReadAsStringAsync(cancellation);

        logger.LogDebug("Call to {route} ended with status {status}.\n\r{data}", route, response.StatusCode, data);
    }
}