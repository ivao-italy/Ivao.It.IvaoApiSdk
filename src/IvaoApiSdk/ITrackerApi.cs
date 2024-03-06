namespace Ivao.It.IvaoApiSdk;

public interface ITrackerApi
{
    Task GetAtcSummary(CancellationToken cancellation = default);
}