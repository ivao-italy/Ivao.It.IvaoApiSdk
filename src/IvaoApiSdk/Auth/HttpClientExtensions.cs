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
        const string authHeader = "Authorization";
        cl.DefaultRequestHeaders.TryGetValues(authHeader, out var items);
        var header = items?.SingleOrDefault();
        var newHeaderValue = $"Bearer {token.AccessToken}";

        if (header is not null && header != newHeaderValue)
        {
            cl.DefaultRequestHeaders.Remove(authHeader);
        }

        if (header != newHeaderValue)
        {
            cl.DefaultRequestHeaders.Add(authHeader, newHeaderValue);
        }

        return cl;
    }

    ///// <summary>
    ///// Adds the API Key (LEGACY) to the HttpClient
    ///// </summary>
    ///// <param name="cl"></param>
    ///// <param name="config"></param>
    ///// <returns></returns>
    //public static HttpClient AddApiKey(this HttpClient cl, IvaoApiConfig config)
    //{
    //    ArgumentException.ThrowIfNullOrEmpty(config.ApiKey);
    //    cl.DefaultRequestHeaders.Add("apiKey", config.ApiKey);
    //    return cl;
    //}

}
