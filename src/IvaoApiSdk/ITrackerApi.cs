namespace Ivao.It.ApiSdk;

public interface ITrackerApi
{
    Task GetAtcSummary(CancellationToken cancellation = default);
}