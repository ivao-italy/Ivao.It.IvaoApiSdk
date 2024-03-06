namespace Ivao.It.IvaoApiSdk.Dto;
public class FlightPlanListDto
{
    public double Id { get; set; }

    public string Callsign { get; set; } = null!;

    public int UserId { get; set; }

    public string AircraftId { get; set; } = null!;

    public string DepartureId { get; set; } = null!;

    public string ArrivalId { get; set; } = null!;

    public DateTimeOffset Eobt { get; set; }

    public bool IsArchived { get; set; }

}
