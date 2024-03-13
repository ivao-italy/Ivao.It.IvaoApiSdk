namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class FlightPlanSummaryDto
{
    public int Id { get; set; }
    public string ArrivalId { get; set; } = null!;
    public string DepartureId { get; set; } = null!;
    public string AircraftId { get; set; } = null!;
    public AircraftSummaryDto Aircraft { get; set; } = null!;
    public BaseAirportDto Departure { get; set; } = null!;
    public BaseAirportDto Arrival { get; set; } = null!;
    public BaseAirportDto Alternative { get; set; } = null!;
    public BaseAirportDto Alternative2 { get; set; } = null!;
}