using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;

namespace EVM.Services.Extensions;

public static class AuthorizationServiceExtensions
{
    public static async Task<bool> CanPerformActionAsync(this IAuthorizationService authorizationService, ClaimsPrincipal user, string action, string entity)
    {
        if (user?.Identity == null || !user.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var policyName = $"{action}{entity}Policy";

        var result = await authorizationService.AuthorizeAsync(user, null, policyName);

        return result.Succeeded;
    }
}
