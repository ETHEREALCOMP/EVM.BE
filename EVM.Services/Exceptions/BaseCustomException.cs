using System.Net;

namespace EVM.Services.Exceptions;

public class BaseCustomException : Exception
{
    public string? InternalMessage { get; set; }

    public HttpStatusCode Code { get; set; }

    public BaseCustomException(string publicMessage, string? _internalMessage = null, HttpStatusCode _code = HttpStatusCode.InternalServerError)
        : base(publicMessage)
    {
    }

    public BaseCustomException(string publicMessage, HttpStatusCode code)
        : base(publicMessage)
    {
        Code = code;
    }
}