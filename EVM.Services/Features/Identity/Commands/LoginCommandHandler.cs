using EVM.Data.Models.IdentityFeature;
using EVM.Services.Features.Identity.Models.Requests;
using EVM.Services.Features.Identity.Models.Responses;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EVM.Services.Features.Identity.Commands;

public class LoginCommandHandler(UserManager<User> _userManager, IHttpContextAccessor _httpContextAccessor, SignInManager<User> _signInManager, ClaimsService _claimsService, JwtService _jwtService)
    : IRequestHandler<LoginRequest, ApiResponse<BaseAuthResponse>>
{
    public async Task<ApiResponse<BaseAuthResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new(HttpStatusCode.Unauthorized, "Invalid credentials");
        }

        var loginResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (loginResult.Succeeded)
        {
            var claims = await _claimsService.GenerateUserClaimsAsync(user);
            var token = _jwtService.GenerateToken(user, claims);

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                httpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,  // Токен недоступний у JavaScript
                    Secure = true,  // Використовувати лише HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
            }

            return new(new() { Id = user.Id, Token = token });
        }

        return new(HttpStatusCode.Unauthorized, "Invalid credentials");
    }
}
