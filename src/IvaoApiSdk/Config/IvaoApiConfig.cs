namespace Ivao.It.ApiSdk.Config;
public class IvaoApiConfig
{
    public static string SectionName = "IvaoApi";

    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string? ApiKey { get; set; }
    public string[] Scopes { get; set; } = null!;
}
