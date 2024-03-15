using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Ivao.It.IvaoApiSdk.Auth;

[DebuggerDisplay($"Iss: {{{nameof(Issued)}}} - Exp: {{{nameof(Expired)}}} - {{{nameof(Expired)}}}")]
internal class AuthResponse
{
#if NET7_0
    [JsonPropertyName("access_token")]
#endif
    public string AccessToken { get; set; } = null!;
#if NET7_0
    [JsonPropertyName("token_type")]
#endif
    public string TokenType { get; set; } = null!;
#if NET7_0
    [JsonPropertyName("expires_in")]
#endif
    public int ExpiresIn { get; set; }
    public DateTime Issued { get; } = DateTime.UtcNow;
    public bool Expired => DateTime.UtcNow >= Issued.AddSeconds(ExpiresIn);
}