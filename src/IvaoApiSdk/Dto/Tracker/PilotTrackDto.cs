namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class PilotTrackDto
{
    public double Time { get; set; }
    public DateTime Timestamp { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Altitude { get; set; }
    public int AltitudeDifference { get; set; }
    public double ArrivalDistance { get; set; }
    public int Bank { get; set; }
    public double DepartureDistance { get; set; }
    public int GroundSpeed { get; set; }
    public int Heading { get; set; }
    public bool OnGround { get; set; }
    public double Pitch { get; set; }
    public bool Sandbagging { get; set; }
    public string? State { get; set; } //TODO Code?
    public int Transponder { get; set; }
    public string TransponderMode { get; set; } = null!;
}