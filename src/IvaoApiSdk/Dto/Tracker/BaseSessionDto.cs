using System.Collections.ObjectModel;

namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class BaseSessionDto
{
    public int Id { get; set; }
    public string Callsign { get; set; } = null!;
    public uint UserId { get; set; }
    public string ConnectionType { get; set; } = null!;
    public string ServerId { get; set; } = null!;
    public ulong Time { get; set; }
    public string SoftwareTypeId { get; set; } = null!;
    public string SoftwareVersion { get; set; } = null!;
    public bool Sandbagging { get; set; }
    public bool IsMilitary { get; set; }
    public bool IsWorldTour { get; set; }
    public bool IsCompleted { get; set; }
    public ICollection<FlightPlanSummaryDto> FlightPlans { get; set; } = new Collection<FlightPlanSummaryDto>();
    public SoftwareSummaryDto SoftwareType { get; set; } = null!;
    public UserSummaryDto User { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime CompletedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}