using System.Net.Http.Json;

using Ivao.It.IvaoApiSdk.Dto;
using Ivao.It.IvaoApiSdk.Exceptions;

using Microsoft.Extensions.Logging;

namespace Ivao.It.IvaoApiSdk;

internal static class HttpClientExtensions
{
    /// <summary>
    /// Checks status, adding loggin, exception wrapping and Gateway Response reading for details from IVAO Api
    /// </summary>
    /// <param name="message"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    /// <exception cref="IvaoApiException"></exception>
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