namespace Ivao.It.ApiSdk.Exceptions;

public class IvaoApiException : Exception
{
    public IvaoApiException() { }
    public IvaoApiException(string message) : base(message) { }
    public IvaoApiException(string message, Exception inner) : base(message, inner) { }
}