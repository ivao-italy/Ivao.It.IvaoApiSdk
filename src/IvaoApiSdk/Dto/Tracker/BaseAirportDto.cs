namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class BaseAirportDto
{
    public string Icao { get; set; } = null!;
    public string Iata { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string CountryId { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool Military { get; set; }
}