using EVM.Data.Models.IdentityFeature;
using EVM.Services.Features.Identity.Models.Requests;
using EVM.Services.Features.Identity.Models.Responses;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Service;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EVM.Services.Features.Identity.Commands;

public class SigninCommand(
    UserManager<User> _userManager,
    SignInManager<User> _signInManager,
    ClaimsService _claimsService)
{
    public async Task<ApiResponse<SigninResponse>> ExecuteAsync(SigninRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            return new(HttpStatusCode.Unauthorized, "Invalid credentials");
        }

        var loginResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (loginResult.Succeeded)
        {
            var claims = await _claimsService.GenerateUserClaimsAsync(user);
            await _signInManager.SignInWithClaimsAsync(user, isPersistent: true, claims);
            return new(new() { UserId = user.Id });
        }
        else if (loginResult.RequiresTwoFactor)
        {
            return new(HttpStatusCode.Unauthorized, "2 Factor Authentication required", data: new() { Requires2FA = true });
        }
        else if (loginResult.IsLockedOut)
        {
            return new(HttpStatusCode.Unauthorized, "Account is locked", data: new() { IsLocked = true });
        }

        return new(HttpStatusCode.Unauthorized, "Invalid credentials");
    }
}
