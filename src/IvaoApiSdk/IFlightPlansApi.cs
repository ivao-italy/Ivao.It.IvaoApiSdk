using Ivao.It.IvaoApiSdk.Dto;

namespace Ivao.It.IvaoApiSdk;

[Obsolete("Not supported")]
public interface IFlightPlansApi
{
    Task<List<FlightPlanListDto>?> GetUsersFlightPlans(string vid, CancellationToken cancellation = default);
}