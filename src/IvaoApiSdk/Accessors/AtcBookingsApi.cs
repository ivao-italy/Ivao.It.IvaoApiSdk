using System.Text.Json;

using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Dto;
using Ivao.It.IvaoApiSdk.Json;

using Microsoft.Extensions.Logging;

namespace Ivao.It.IvaoApiSdk.Accessors;

#if NET8_0_OR_GREATER
internal class AtcBookingsApi(
    IAuthenticator authenticator,
    ILogger<AtcBookingsApi> logger,
    HttpClient client
) : BaseAccessor(logger), IAtcBookingsApi
{
#else
internal class AtcBookingsApi : BaseAccessor, IAtcBookingsApi
{
    private readonly IAuthenticator authenticator;
    private readonly HttpClient client;

    public AtcBookingsApi(
        IAuthenticator authenticator,
        ILogger<AtcBookingsApi> logger,
        HttpClient client) : base(logger)
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
               JsonOptions,
               cancellation);

        if (icaoFilter is not null)
        {
            data = data?
                .Where(d => d.AtcCallsign?.StartsWith(icaoFilter, StringComparison.OrdinalIgnoreCase) ?? false)
                .ToList();
        }

        return data;
    }
}