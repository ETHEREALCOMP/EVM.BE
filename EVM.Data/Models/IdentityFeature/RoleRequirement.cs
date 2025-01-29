using EVM.Data.Enums;
using Microsoft.AspNetCore.Authorization;

namespace EVM.Data.Models.IdentityFeature;

public class RoleRequirement(UserRole requiredRole) : IAuthorizationRequirement
{
    public UserRole RequiredRole { get; } = requiredRole;
}
