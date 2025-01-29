using EVM.Data.Models.IdentityFeature;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace EVM.Services.Service;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == requirement.RequiredRole.ToString()))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}