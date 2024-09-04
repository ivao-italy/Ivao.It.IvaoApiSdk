namespace Ivao.It.IvaoApiSdk.Dto;
internal class PagedResult<TObj> where TObj : class
{
    public int TotalItems { get; set; }
    public int PerPage { get; set; }
    public int Page { get; set; }
    public int Pages { get; set; }
    public List<TObj> Items { get; set; }
}
