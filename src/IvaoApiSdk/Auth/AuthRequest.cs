using System.Text.Json.Serialization;

using Ivao.It.IvaoApiSdk.Config;

namespace Ivao.It.IvaoApiSdk.Auth;

internal class AuthRequest
{
#if NET7_0
    [JsonPropertyName("grant_type")]
#endif
    public string GrantType { get; set; } = "client_credentials";

#if NET7_0
    [JsonPropertyName("client_id")]
#endif
    public string ClientId { get; set; } = null!;

#if NET7_0
    [JsonPropertyName("client_secret")]
#endif
    public string ClientSecret { get; set; } = null!;

    public string Scope { get; set; } = null!;

    private AuthRequest()
    {
    }

    public static AuthRequest FromConfig(IvaoApiConfig config) => new()
    {
        ClientId = config.ClientId,
        ClientSecret = config.ClientSecret,
        Scope = string.Join(' ', config.Scopes)
    };
}