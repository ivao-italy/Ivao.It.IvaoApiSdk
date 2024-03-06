namespace Ivao.It.IvaoApiSdk.Auth;

internal interface IAuthenticator
{
    Task<AuthResponse> GetToken(CancellationToken cancellation);
}