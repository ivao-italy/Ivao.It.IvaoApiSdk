using Ivao.It.IvaoApiSdk.Dto;

namespace Ivao.It.IvaoApiSdk;

/// <summary>
/// IVAO Atc Bookings
/// </summary>
public interface IAtcBookingsApi
{
    /// <summary>
    /// Get ATC bookings by day
    /// </summary>
    /// <param name="icaoFilter">Used as StartsWith. E.g. "LI" returns italian ICAO codes</param>
    /// <param name="date">If not provided, takes today</param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<ICollection<BookingResponseDto>?> GetDailyAtcSchedules(
        string? icaoFilter = null,
        DateTime? date = null,
        CancellationToken cancellation = default);
}