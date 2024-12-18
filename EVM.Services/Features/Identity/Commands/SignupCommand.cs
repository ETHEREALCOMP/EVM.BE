using EVM.Data;
using EVM.Data.Models.IdentityFeature;
using EVM.Services.Features.Identity.Models.Requests;
using EVM.Services.Features.Identity.Models.Responses;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Service;
using Microsoft.AspNetCore.Identity;

namespace EVM.Services.Features.Identity.Commands;

public class SignupCommand(SignInManager<User> _signInManager,
    CreateUserService _createUserService,
    AppDbContext _dbContext,
     ClaimsService _claimsService)
{
    public async Task<ApiResponse<SignupResponse>> ExecuteAsync(SignupRequest request, CancellationToken cancellationToken = default)
    {
        var newUser = new User()
        {
            Email = request.Email,
            UserName = request.UserName,
            Name = request.Name,
        };

        await _createUserService.CreateAsync(newUser, request.Password, cancellationToken);

        var claims = await _claimsService.GenerateUserClaimsAsync(newUser);
        await _signInManager.SignInWithClaimsAsync(newUser, isPersistent: true, claims);
        return new(new() { UserId = newUser.Id });
    }
}
