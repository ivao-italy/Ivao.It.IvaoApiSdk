using System.Net.Http.Json;

using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Config;
using Ivao.It.IvaoApiSdk.Dto;
using Ivao.It.IvaoApiSdk.Exceptions;

using Microsoft.Extensions.Logging;

namespace Ivao.It.IvaoApiSdk;

internal static class HttpClientExtensions
{
    public static HttpClient AddToken(this HttpClient cl, AuthResponse token)
    {
        cl.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        return cl;
    }

    public static HttpClient AddApiKey(this HttpClient cl, IvaoApiConfig config)
    {
        ArgumentException.ThrowIfNullOrEmpty(config.ApiKey);
        cl.DefaultRequestHeaders.Add("apiKey", config.ApiKey);
        return cl;
    }


    public static async Task<HttpResponseMessage> EnsureSuccessOrWrap(this HttpResponseMessage message, ILogger logger)
    {
        try
        {
            message.EnsureSuccessStatusCode();
            return message;
        }
        catch (Exception e)
        {
            var body = await message.Content.ReadFromJsonAsync<GatewayResponsesDto>();
            logger.LogError(e,
                "Call to IVAO Api failed: {body}\n\rStatus: {status} - {reason}",
                body!.Message,
                message.StatusCode,
                message.ReasonPhrase);

            throw new IvaoApiException($"Call to IVAO Api failed: {body.Message}", e);
        }
    }
}