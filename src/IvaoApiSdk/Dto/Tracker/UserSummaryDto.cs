namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class UserSummaryDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string DivisionId { get; set; } = null!;
    public UserRatingDto Rating { get; set; } = new UserRatingDto();
}