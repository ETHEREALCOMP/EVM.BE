using EVM.Data.Models.IdentityFeature;
using EVM.Services.Exceptions;
using EVM.Services.Features.Identity.Models.Const;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EVM.Services.Service;

public class CustomClaimsValidator(IHttpContextAccessor _httpContextAccessor, ClaimsService _claimsService, UserManager<User> _userManager)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        if (!ShouldRenewCookie(context))
        {
            return;
        }

        var userIdClaim = context.Principal?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            context.RejectPrincipal();
            await _httpContext.SignOutAsync();
            return;
        }

        var user = await _userManager.FindByIdAsync(userIdClaim.Value);
        if (user == null)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync();
            return;
        }

        var claims = await _claimsService.GenerateUserClaimsAsync(user);
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        context.ReplacePrincipal(principal);
        context.ShouldRenew = true;
    }

    private static bool ShouldRenewCookie(CookieValidatePrincipalContext context)
    {
        var issuedUtc = context.Properties.IssuedUtc;
        if (!issuedUtc.HasValue)
        {
            return true;
        }

        var timeElapsed = DateTimeOffset.UtcNow - issuedUtc.Value;
        return timeElapsed > (AuthSettings.CookiesExpiration / 2);
    }
}
