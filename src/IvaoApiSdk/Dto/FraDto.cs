namespace Ivao.It.IvaoApiSdk.Dto;

public class FraDto
{
    //public DateTime createdAt { get; set; }
    //public DateTime updatedAt { get; set; }
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int? AtcPositionId { get; set; }
    public int? SubcenterId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool DayMon { get; set; }
    public bool DayTue { get; set; }
    public bool DayWed { get; set; }
    public bool DayThu { get; set; }
    public bool DayFri { get; set; }
    public bool DaySat { get; set; }
    public bool DaySun { get; set; }
    //public object Date { get; set; }
    public int MinAtc { get; set; }
    public bool Active { get; set; }
    //public object user_id { get; set; }
    //public int atc_position_id { get; set; }
    //public int subcenter_id { get; set; }
    //public int min_atc { get; set; }
    public AtcPositionDto? AtcPosition { get; set; }
    public SubcenterDto? Subcenter { get; set; }

    public override string ToString() => $"{AtcPosition?.ComposePosition ?? Subcenter?.ComposePosition} - {StartTime} - {EndTime} @ {MinAtc}";
}
