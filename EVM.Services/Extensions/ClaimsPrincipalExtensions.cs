using EVM.Services.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EVM.Services.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetId(this ClaimsPrincipal user)
    {
        var name = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return name == null ? null : Guid.Parse(name);
    }
}
