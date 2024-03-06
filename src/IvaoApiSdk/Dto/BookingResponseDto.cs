using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Ivao.It.IvaoApiSdk.Dto;

[DebuggerDisplay($"{{{nameof(AtcCallsign)}}} - {{{nameof(StartDate)}}} to {{{nameof(EndDate)}}}")]
public class BookingResponseDto
{
    public int Id { get; set; }
    public UserSubQueryDto User { get; set; } = new UserSubQueryDto();
    public string? AtcPosition => AtcPositionRef?.ComposePosition;
    public AtcPositionDto AtcPositionRef { get; set; } = new AtcPositionDto();
    public string? Subcenter => SubcenterRef?.ComposePosition;
    public SubcenterDto SubcenterRef { get; set; } = new SubcenterDto();
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public bool Voice { get; set; }
    [JsonInclude]
    internal string? Training { get; set; }
    public bool IsTraining => Training == "training";
    public bool IsExam => Training == "exam";
    public DateTimeOffset CreatedAt { get; set; }
    public string? AtcCallsign => AtcPositionRef?.ComposePosition ?? SubcenterRef?.ComposePosition;
}

public class UserSubQueryDto
{
    public int Id { get; set; }
    public string DivisionId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}   

public class AtcPositionDto
{
    public long Id { get; set; }
    public string AirportId { get; set; } = null!;
    public string AtcCallsign { get; set; } = null!;
    public bool Military { get; set; }
    public double Frequency { get; set; }
    public string ComposePosition { get; set; } = null!;
}

public class SubcenterDto
{
    public long Id { get; set; }
    public string CenterId { get; set; } = null!;
    public string AtcCallsign { get; set; } = null!;
    public bool Military { get; set; }
    public double Frequency { get; set; }
    public string ComposePosition { get; set; } = null!;
}