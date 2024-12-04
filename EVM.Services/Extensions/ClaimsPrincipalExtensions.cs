using System.Security.Claims;

namespace EVM.Services.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetId(this ClaimsPrincipal principal)
    {
        var name = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        return name == null ? null : Guid.Parse(name);
    }
}
