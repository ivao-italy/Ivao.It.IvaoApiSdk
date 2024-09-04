using System.Text.Json;

using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Dto;
using Ivao.It.IvaoApiSdk.Json;

using Microsoft.Extensions.Logging;

namespace Ivao.It.IvaoApiSdk.Accessors;
#if NET8_0_OR_GREATER

internal class CoreApi(IAuthenticator authenticator,
    ILogger<AtcBookingsApi> logger,
    HttpClient client)
    : BaseAccessor(logger), ICoreApi
{
#else
internal class CoreApi : BaseAccessor, ICoreApi
{
    private readonly IAuthenticator authenticator;
    private readonly HttpClient client;

    public CoreApi(
        IAuthenticator authenticator,
        ILogger<AtcBookingsApi> logger,
        HttpClient client) : base(logger)
    {
        this.authenticator = authenticator;
        this.client = client;
    }
#endif
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new BoolConverter() }
    };


    public async Task<List<FraDto>> GetAllFras(string icaoStart, CancellationToken ct)
    {
        //Max 100 x page
        //v2/fras?page=1&perPage=100&startsWith=LI&isActive=true&members=false&positions=true&expand=true
        const string route = @"v2/fras";
        var page = 1;
        var data = await ReadPage(page);
        var fras = data!.Items.ToList();

        for (page = 2; page <= data.Pages; page++)
        {
            fras.AddRange((await ReadPage(page))!.Items.ToList());
        }

        return fras;

        async Task<PagedResult<FraDto>> ReadPage(int page)
        {
            var withQuery = $"{route}?page={page}&perPage=100&startsWith={icaoStart}&isActive=true&members=false&positions=true&expand=true";
            var data = await RunApiCall<PagedResult<FraDto>>(async () =>
                await client.AddToken(await authenticator.GetToken(ct)).GetAsync(withQuery, ct),
                route,
                JsonOptions,
                ct);
            return data!;
        }
    }
}
