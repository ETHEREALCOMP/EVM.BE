using System.Net;

namespace EVM.Services.Exceptions;

public class ResourceUnauthorizedAccessException(string publicMessage) : BaseCustomException(publicMessage, HttpStatusCode.Unauthorized)
{
}
