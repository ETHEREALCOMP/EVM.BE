using EVM.Data.Models.IdentityFeature;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace EVM.Services.Features.Identity.Commands;

public class SignoutCommand(SignInManager<User> _signInManager)
{
    public async Task<ApiResponse> ExecuteAsync()
    {
        await _signInManager.SignOutAsync();

        return new();
    }
}
