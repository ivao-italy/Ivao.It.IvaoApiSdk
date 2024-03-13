namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class AircraftSummaryDto
{
    public string IcaoCode { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string WakeTurbulence { get; set; } = null!;
    public bool IsMilitary { get; set; }
    public string Description { get; set; } = null!;
}