using Ivao.It.IvaoApiSdk.Config;

namespace Ivao.It.IvaoApiSdk.Auth;
internal static class HttpClientExtensions
{
    /// <summary>
    /// Adds the bearer token to the HttpClient
    /// </summary>
    /// <param name="cl"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static HttpClient AddToken(this HttpClient cl, AuthResponse token)
    {
        cl.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        return cl;
    }

    /// <summary>
    /// Adds the API Key (LEGACY) to the HttpClient
    /// </summary>
    /// <param name="cl"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static HttpClient AddApiKey(this HttpClient cl, IvaoApiConfig config)
    {
        ArgumentException.ThrowIfNullOrEmpty(config.ApiKey);
        cl.DefaultRequestHeaders.Add("apiKey", config.ApiKey);
        return cl;
    }

}
