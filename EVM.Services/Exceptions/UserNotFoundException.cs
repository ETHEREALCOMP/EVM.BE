using System.Net;

namespace EVM.Services.Exceptions;

public class UserNotFoundException(string? _userIdentifierSource = "HttpContext")
    : BaseCustomException("User not found", $"User from {_userIdentifierSource} not found", HttpStatusCode.NotFound)
{
}