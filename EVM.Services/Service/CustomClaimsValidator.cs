using EVM.Data.Models.IdentityFeature;
using EVM.Services.Exceptions;
using EVM.Services.Features.Identity.Models.Const;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace EVM.Services.Service;

public class CustomClaimsValidator(ILogger<CustomClaimsValidator> _logger, IHttpContextAccessor _httpContextAccessor, ClaimsService _claimsService, UserManager<User> _userManager)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task ValidateClaims()
    {
        var user = await _userManager.GetUserAsync(_httpContext.User);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var claims = await _claimsService.GenerateUserClaimsAsync(user);
        var identity = new ClaimsIdentity(claims, "Custom");
        _httpContext.User.AddIdentity(identity);
    }
}
