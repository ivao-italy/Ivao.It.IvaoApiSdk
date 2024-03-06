using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Dto;

using Microsoft.Extensions.Logging;

namespace Ivao.It.IvaoApiSdk.Accessors;

internal class FlightPlansApi(
    IAuthenticator authenticator,
    ILogger<AtcBookingsApi> logger,
    HttpClient client
) : BaseAccessor(logger), IFlightPlansApi
{

    public async Task<List<FlightPlanListDto>?> GetUsersFlightPlans(string vid, CancellationToken cancellation = default)
    {
        string route = @$"v2/users/{vid}/flightPlans";

        //TODO Querystring

        var data = await RunApiCall<List<FlightPlanListDto>>(async () =>
                await client.AddToken(await authenticator.GetToken(cancellation))
                            .GetAsync(route, cancellation),
            route,
            cancellation);

        return data;
    }
}
