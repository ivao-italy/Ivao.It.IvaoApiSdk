using System.Collections.ObjectModel;

namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class PaginatedSessionsDto
{
    public int TotalItems { get; set; }
    public int PerPage { get; set; }
    public int Page { get; set; }
    public int Pages { get; set; }
    public ICollection<BaseSessionDto> Items { get; set; } = new Collection<BaseSessionDto>();
}