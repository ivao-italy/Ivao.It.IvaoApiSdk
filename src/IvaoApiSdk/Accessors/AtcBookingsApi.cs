using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Config;
using Ivao.It.IvaoApiSdk.Dto;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ivao.It.IvaoApiSdk.Accessors;

internal class AtcBookingsApi(
    IAuthenticator authenticator,
    ILogger<AtcBookingsApi> logger,
    HttpClient client
) : BaseAccessor(logger), IAtcBookingsApi
{
    public async Task<ICollection<BookingResponseDto>?> GetDailyAtcSchedules(
        string? icaoFilter = null,
        DateTime? date = null,
        CancellationToken cancellation = default)
    {
        const string route = @"v2/atc/bookings/daily";

        var data = await RunApiCall<List<BookingResponseDto>>(async () =>
                await client
                    .AddToken(await authenticator.GetToken(cancellation))
                    .GetAsync(date is not null ? $"{route}?date={date:yyyy-M-d}" : route, cancellation),
               route,
               cancellation);

        return data;
    }
}