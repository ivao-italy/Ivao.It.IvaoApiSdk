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

    Task<List<FlightPlanDto>?> GetSessionFlightPlans(int sessionId, CancellationToken cancellation = default);
}