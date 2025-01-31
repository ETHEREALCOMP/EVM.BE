using EVM.Data.Models.IdentityFeature;
using EVM.Services.Features.Identity.Models.Requests;
using EVM.Services.Features.Identity.Models.Responses;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Service;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EVM.Services.Features.Identity.Commands;

public class LoginCommandHandler(UserManager<User> _userManager, SignInManager<User> _signInManager, ClaimsService _claimsService, JwtService _jwtService)
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
            await _signInManager.SignInWithClaimsAsync(user, isPersistent: true, claims);
            return new(new() { Id = user.Id, Token = _jwtService.GenerateToken(user, claims) });
        }
        else if (loginResult.RequiresTwoFactor)
        {
            return new(HttpStatusCode.Unauthorized, "2 Factor Authentication required");
        }
        else if (loginResult.IsLockedOut)
        {
            return new(HttpStatusCode.Unauthorized, "Account is locked");
        }

        return new(HttpStatusCode.Unauthorized, "Invalid credentials");
    }
}
