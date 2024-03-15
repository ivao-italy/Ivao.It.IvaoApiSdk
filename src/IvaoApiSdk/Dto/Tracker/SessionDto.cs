namespace Ivao.It.IvaoApiSdk.Dto.Tracker;
public class SessionDto
{
    public BaseSessionDto Session { get; init; } = null!;
    public IList<FlightPlanDto>? FlightPlans { get; init; } = new List<FlightPlanDto>();
    public IList<PilotTrackDto>? Tracks { get; init; }

}
