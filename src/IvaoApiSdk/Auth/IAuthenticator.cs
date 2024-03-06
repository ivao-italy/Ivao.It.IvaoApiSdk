namespace Ivao.It.ApiSdk.Auth;

internal interface IAuthenticator
{
    Task<AuthResponse> GetToken(CancellationToken cancellation);
}