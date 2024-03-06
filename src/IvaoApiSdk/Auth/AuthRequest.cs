using Ivao.It.IvaoApiSdk.Config;

namespace Ivao.It.IvaoApiSdk.Auth;

internal class AuthRequest
{
    public string GrantType { get; set; } = "client_credentials";

    public string ClientId { get; set; } = null!;

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