using System.Runtime.CompilerServices;
using System.Text.Json;

using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Dto.Tracker;
using Ivao.It.IvaoApiSdk.Json;

using Microsoft.Extensions.Logging;

namespace Ivao.It.IvaoApiSdk.Accessors;

#if NET8_0_OR_GREATER
internal class TrackerApi(
    IAuthenticator authenticator,
    ILogger<TrackerApi> logger,
    HttpClient client)
    : BaseAccessor(logger), ITrackerApi
{
#else
internal class TrackerApi : BaseAccessor, ITrackerApi
{
    private readonly IAuthenticator authenticator;
    private readonly HttpClient client;
    public TrackerApi(IAuthenticator authenticator,
        ILogger<TrackerApi> logger,
        HttpClient client) 
        : base(logger)
    {
        this.authenticator = authenticator;
        this.client = client;
    }
#endif

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new BoolConverter() }
    };

    public async Task GetAtcSummary(CancellationToken cancellation = default)
    {
        const string route = @"v2/tracker/now/atc/summary";

        await RunApiCall<List<FlightPlanDto>>(async () =>
                await client
                    .AddToken(await authenticator.GetToken(cancellation))
                    .GetAsync(route, cancellation),
            route,
            cancellation);
    }

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
            JsonOptions,
            cancellation);

        return data;
    }

    public async Task<BaseSessionDto?> GetSession(int sessionId, CancellationToken cancellation = default)
    {
        const string route = @"v2/tracker/sessions";
        var withQuery = $@"{route}/{sessionId}";

        var data = await RunApiCall<BaseSessionDto>(async () =>
                await client.AddToken(await authenticator.GetToken(cancellation)).GetAsync(withQuery, cancellation),
            route,
            JsonOptions,
            cancellation);

        return data;
    }
    
    public async Task<List<FlightPlanDto>?> GetSessionFlightPlans(int sessionId, CancellationToken cancellation = default)
    {
        string route = @$"v2/tracker/sessions/{sessionId}/flightPlans";

        var data = await RunApiCall<List<FlightPlanDto>>(async () =>
                    await client
                        .AddToken(await authenticator.GetToken(cancellation))
                        .GetAsync(route, cancellation),
            route,
            JsonOptions,
            cancellation);

        return data;
    }

    public async Task<List<PilotTrackDto>?> GetSessionTracks(int sessionId, CancellationToken cancellation = default)
    {
        string route = @$"v2/tracker/sessions/{sessionId}/tracks";

        var data = await RunApiCall<List<PilotTrackDto>>(async () =>
                await client
                    .AddToken(await authenticator.GetToken(cancellation))
                    .GetAsync(route, cancellation),
            route,
            JsonOptions,
            cancellation);

        return data;
    }

    public async Task<SessionDto?> GetFullSessionData(int sessionId, CancellationToken cancellation = default)
    {
        var session = await GetSession(sessionId, cancellation);
        if (session == null) return null;

        var tracks = await GetSessionTracks(sessionId, cancellation);
        var fpls = await GetSessionFlightPlans(sessionId, cancellation);

        fpls?.ForEach(f =>
        {
            f.Callsign = session.Callsign;
        });

        return new SessionDto { Session = session, Tracks = tracks, FlightPlans = fpls };
    }
}