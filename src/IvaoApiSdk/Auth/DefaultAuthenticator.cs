using System.Net;
using System.Net.Http.Json;
using System.Security.Authentication;
using System.Text.Json;

using Ivao.It.IvaoApiSdk.Config;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ivao.It.IvaoApiSdk.Auth;

internal class DefaultAuthenticator(
    IOptions<IvaoApiConfig> config,
    HttpClient client,
    ILogger<DefaultAuthenticator> logger) : IAuthenticator
{
    private const string DefaultTokenEndpoint = @"v2/oauth/token";

    
    private static AuthResponse? LatestResponse;

    public async Task<AuthResponse> GetToken(CancellationToken cancellation)
    {
        //Token already existing and valid
        if (LatestResponse is not null && !LatestResponse.Expired)
        {
            logger.LogInformation("Reusing existing Token");
            return LatestResponse;
        }

        //Get a fresh one
        JsonSerializerOptions o = new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        var req = AuthRequest.FromConfig(config.Value);
        var response = await client.PostAsJsonAsync(
            DefaultTokenEndpoint, 
            req,
            o
            , cancellation);
        
        //request.EnsureSuccessStatusCode(); //TODO Better handling?
        if (response.StatusCode >= HttpStatusCode.BadRequest)
        {
            var cont = await response.Content.ReadAsStringAsync(cancellation);
            logger.LogError("Failed to authenticate to IVAO with status {status} and message: {message}", response.StatusCode, cont);
            throw new InvalidCredentialException();
        }

        LatestResponse = await response.Content.ReadFromJsonAsync<AuthResponse>(cancellation);
        logger.LogInformation("Got a new Token from IVAO API\n\rFor scopes: {scopes}\n\rwith request:\n\r{request}", 
            req.Scope, 
            response.RequestMessage?.ToString());
        return LatestResponse!;
    }
}