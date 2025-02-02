using EVM.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EVM.Services.Extensions;

public static class AuthorizationServiceExtensions
{
    public static async Task<bool> CanCreateEvent(this IAuthorizationService authorizationService, ClaimsPrincipal user)
    {
        if (user?.Identity == null || !user.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var result = await authorizationService.AuthorizeAsync(user, null, "CreateEventPolicy");

        return result.Succeeded;
    }
}
