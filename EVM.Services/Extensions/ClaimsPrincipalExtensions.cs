using EVM.Services.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EVM.Services.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal user)
    {
        var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userIdClaim != null ? Guid.Parse(userIdClaim)
            : throw new UserNotFoundException("User ID not found.");
    }
}
