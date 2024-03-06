using Ivao.It.IvaoApiSdk.Dto;

namespace Ivao.It.IvaoApiSdk;

public interface IFlightPlansApi
{
    Task<List<FlightPlanListDto>?> GetUsersFlightPlans(string vid, CancellationToken cancellation = default);
}