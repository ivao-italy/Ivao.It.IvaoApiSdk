﻿using System.Diagnostics;

namespace Ivao.It.ApiSdk.Auth;

[DebuggerDisplay($"Iss: {{{nameof(Issued)}}} - Exp: {{{nameof(Expired)}}} - {{{nameof(Expired)}}}")]
internal class AuthResponse
{
    public string AccessToken { get; set; } = null!;
    public string TokenType { get; set; } = null!;
    public int ExpiresIn { get; set; }
    public DateTime Issued { get; } = DateTime.UtcNow;
    public bool Expired => DateTime.UtcNow >= Issued.AddSeconds(ExpiresIn);

    public override string ToString() => AccessToken;
}