using Ivao.It.IvaoApiSdk.Dto.Tracker;

namespace Ivao.It.IvaoApiSdk;

public interface ITrackerApi
{
    Task GetAtcSummary(CancellationToken cancellation = default);

    /// <summary>
    /// Gets users data from tracker
    /// </summary>
    /// <param name="vid">User VID</param>
    /// <param name="connectionType">Defaults to Pilot</param>
    /// <param name="callsign"></param>
    /// <param name="arrivalId"></param>
    /// <param name="departureId"></param>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<PaginatedSessionsDto?> GetSessions(
        string vid,
        TrackerConnectionType? connectionType = null,
        string? callsign = null,
        string? arrivalId = null,
        string? departureId = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellation = default);

    /// <summary>
    /// Gets a session by ID
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<BaseSessionDto?> GetSession(int sessionId, CancellationToken cancellation = default);
    
    /// <summary>
    /// Gets all the flightplans for a session
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<List<FlightPlanDto>?> GetSessionFlightPlans(int sessionId, CancellationToken cancellation = default);

    /// <summary>
    /// Gets the tracking for a session
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<List<PilotTrackDto>?> GetSessionTracks(int sessionId, CancellationToken cancellation = default);

    /// <summary>
    /// Gets all the session data. Calls:<br/> 
    /// <see cref="GetSessionFlightPlans"/><br/>
    /// <see cref="GetSessionTracks"/><br/>
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<SessionDto?> GetFullSessionData(int sessionId, CancellationToken cancellation = default);
}