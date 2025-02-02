using EVM.Data.Models.IdentityFeature;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace EVM.Services.Service;

public class ClaimsService(ILogger<ClaimsService> _logger, IHttpContextAccessor _httpContext)
{
    public async Task<List<Claim>> GenerateUserClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()), // Важливо!
        };

        var claimsIdentity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        _httpContext.HttpContext.User = claimsPrincipal;

        return claims;
    }
}
