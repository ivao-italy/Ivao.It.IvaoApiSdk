namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class UserRatingDto
{
    public bool IsAtc { get; set; }
    public bool IsPilot { get; set; }
    public BaseRatingDto PilotRating { get; set; } = null!;
    public BaseRatingDto AtcRating { get; set; } = null!;
    public NetworkRatingDto NetworkRating { get; set; } = null!;
}