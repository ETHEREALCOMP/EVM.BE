using Microsoft.AspNetCore.Authorization;

namespace EVM.Services.Extensions;

public static class AuthorizationPolicies
{
    public static void AddCrudPolicies(this AuthorizationOptions options)
    {
        AddPolicy(options, "Create", "Event", "EventTask", "Resource");

        AddPolicy(options, "Update", "Event", "EventTask", "Resource");

        AddPolicy(options, "Read", "Event", "EventTask", "Resource");
    }

    private static void AddPolicy(AuthorizationOptions options, string action, params string[] entities)
    {
        foreach (var entity in entities)
        {
            var policyName = $"{action}{entity}Policy";
            options.AddPolicy(policyName, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("role", $"Organizer", $"{action}{entity}");
            });
        }
    }
}
