using System.Collections.ObjectModel;

namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class FlightPlanDto
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public int Revision { get; set; }
    public string AircraftId { get; set; } = null!;
    public int AircraftNumber { get; set; }
    public string DepartureId { get; set; } = null!;
    public string ArrivalId { get; set; } = null!;
    public string AlternativeId { get; set; } = null!;
    public string Alternative2Id { get; set; } = null!;
    public string Route { get; set; } = null!;
    public string Remarks { get; set; } = null!;
    public string Speed { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string FlightRules { get; set; } = null!;
    public string FlightType { get; set; } = null!;
    /// <summary>
    /// Time in seconds
    /// </summary>
    public uint Eet { get; set; }
    /// <summary>
    /// Time in seconds
    /// </summary>
    public uint Endurance { get; set; }
    /// <summary>
    /// Time in seconds
    /// </summary>
    public uint DepartureTime { get; set; }
    /// <summary>
    /// Time in seconds
    /// </summary>
    public uint? ActualDepartureTime { get; set; }
    public ushort PeopleOnBoard { get; set; }
    public ICollection<EquipmentDto> AircraftEquipments { get; set; } = new Collection<EquipmentDto>();
    public string AircraftEquipmentsString => string.Join("", AircraftEquipments.OrderBy(o=>o.Order));
    public ICollection<TransponderDto> AircraftTransponderTypes { get; set; } = new Collection<TransponderDto>();
    public string AircraftTransponderString => string.Join("", AircraftTransponderTypes.OrderBy(o => o.Order));
    public BaseAirportDto Departure { get; set; } = null!;
    public BaseAirportDto Arrival { get; set; } = null!;
    public BaseAirportDto Alternative { get; set; } = null!;
    public BaseAirportDto Alternative2 { get; set; } = null!;
    public AircraftSummaryDto Aircraft { get; set; } = null!;


    public override string ToString()
    {
        var dep = TimeSpan.FromSeconds(DepartureTime);
        var eet = TimeSpan.FromSeconds(Eet);

        return $"(FPL-[CALLSIGN]-{FlightRules}{FlightType}{Environment.NewLine}" +
               $"-{AircraftId}/{Aircraft.WakeTurbulence}-{AircraftEquipmentsString}/{AircraftTransponderString}{Environment.NewLine}" +
               $"-{DepartureId}{dep.Hours:00}{dep.Minutes:00}{Environment.NewLine}" +
               $"-{Speed}{Level} {Route}{Environment.NewLine}" +
               $"-{ArrivalId}{eet.Hours:00}{eet.Minutes:00} {AlternativeId} {Alternative2Id}{Environment.NewLine}" +
               $"-{Remarks})";
    }
}