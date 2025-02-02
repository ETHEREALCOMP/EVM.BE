using EVM.Data.Models.IdentityFeature;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace EVM.Services.Service;

public class CustomClaimsValidator(ILogger<CustomClaimsValidator> _logger, IHttpContextAccessor _httpContextAccessor, ClaimsService _claimsService, UserManager<User> _userManager)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task ValidateClaims()
    {
        var userId = _httpContext.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            _logger.LogWarning("User ID not found in HttpContext");
            foreach (var claim in _httpContext.User.Claims)
            {
                _logger.LogInformation("Claim Type: {ClaimType}, Claim Value: {ClaimValue}", claim.Type, claim.Value);
            }

            throw new UserNotFoundException();
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            _logger.LogWarning("User not found for ID {UserId}", userId);
            throw new UserNotFoundException();
        }

        var claims = await _claimsService.GenerateUserClaimsAsync(user);
        _logger.LogInformation("Generated claims: {Claims}", string.Join(", ", claims.Select(c => $"{c.Type}:{c.Value}")));

        // Оновлюємо ClaimsPrincipal
        var claimsIdentity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        _httpContext.User = claimsPrincipal; // Заміна поточного користувача на новий ClaimsPrincipal
    }
}
