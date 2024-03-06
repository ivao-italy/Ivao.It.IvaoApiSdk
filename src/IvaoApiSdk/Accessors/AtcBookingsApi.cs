using System.Net.Http.Json;

using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Config;
using Ivao.It.IvaoApiSdk.Dto;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ivao.It.IvaoApiSdk.Accessors;

internal class AtcBookingsApi(
    IAuthenticator authenticator,
    ILogger<AtcBookingsApi> logger,
    IOptions<IvaoApiConfig> options,
    HttpClient client
) : IAtcBookingsApi
{
    public async Task<ICollection<BookingResponseDto>?> GetDailyAtcSchedules(
        string? icaoFilter = null,
        DateTime? date = null,
        CancellationToken cancellation = default)
    {
        const string route = @"v2/atc/bookings/daily";

        logger.LogDebug("Calling {route}", route);

        var response = await client
            //.AddToken(await authenticator.GetToken(cancellation))
            .AddApiKey(options.Value)
            .GetAsync(date is not null ? $"{route}?date={date:yyyy-M-d}" : route, cancellation);

        response.EnsureSuccessOrWrap(logger);

        var data = await response.Content.ReadFromJsonAsync<List<BookingResponseDto>>(cancellation);
        if (data is not null && icaoFilter is not null)
        {
            data = data.Where(i =>
                i.AtcCallsign?.StartsWith(icaoFilter, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
        }

        logger.LogInformation("Call to {route} ended with status {status}", route, response.StatusCode, data);
        return data;

        //var str = await response.Content.ReadAsStringAsync(cancellation);
        //logger.LogDebug("Call to {route} ended with status {status}.\n\r{data}", route, response.StatusCode, str);
        //return null;
    }


    private async Task<T> RunApiCall<T>()
    {

    }
}