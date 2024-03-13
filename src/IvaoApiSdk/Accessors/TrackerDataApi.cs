using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Dto;
using Ivao.It.IvaoApiSdk.Dto.Tracker;
using Ivao.It.IvaoApiSdk.Json;

using Microsoft.Extensions.Logging;

namespace Ivao.It.IvaoApiSdk.Accessors;

internal class TrackerApi(
    IAuthenticator authenticator,
    ILogger<TrackerApi> logger,
    HttpClient client) 
    : BaseAccessor(logger), ITrackerApi
{
    public async Task GetAtcSummary(CancellationToken cancellation = default)
    {
        const string route = @"v2/tracker/now/atc/summary";

        logger.LogDebug("Calling {route}", route);

        var response = await client
            .AddToken(await authenticator.GetToken(cancellation)).GetAsync(route, cancellation);

        //Todo deserialize
        var data = await response.Content.ReadAsStringAsync(cancellation);

        logger.LogDebug("Call to {route} ended with status {status}.\n\r{data}", route, response.StatusCode, data);
    }

    /// <inheritdoc/>
    public async Task<PaginatedSessionsDto?> GetSessions(
        string vid,
        TrackerConnectionType? connectionType = null,
        string? callsign = null,
        string? arrivalId = null,
        string? departureId = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellation = default)
    {
        const string route = @"v2/tracker/sessions";
        var withQuery =
            $@"{route}?userId={vid}&callsign={callsign}&from={dateFrom}&to={dateTo}&connectionType={connectionType}&page={pageNumber}&perPage={pageSize}";

        var data = await RunApiCall<PaginatedSessionsDto>(async () =>
                await client.AddToken(await authenticator.GetToken(cancellation)).GetAsync(withQuery, cancellation),
            route,
            cancellation);

        return data;
    }

    public async Task<List<FlightPlanDto>> GetSessionFlightPlans(int sessionId, CancellationToken cancellation = default)
    {
        string route = @$"v2/tracker/sessions/{sessionId}/flightPlans";

        var opt = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, Converters = { new BoolConverter() }};

        var data = await RunApiCall<List<FlightPlanDto>>(async () => 
                    await client.AddToken(await authenticator.GetToken(cancellation)).GetAsync(route, cancellation),
            route,
            opt,
            cancellation);

        return data;
    }
}