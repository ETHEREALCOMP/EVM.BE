using EVM.Data;
using EVM.Data.Models.IdentityFeature;
using EVM.Services.Features.Identity.Models.Requests;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Service;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EVM.Services.Features.Identity.Commands;

public class RegisterCommandHandler(SignInManager<User> _signInManager, CreateUserService _createUserService, AppDbContext _dbContext, ClaimsService _claimsService) 
    : IRequestHandler<RegisterRequest, ApiResponse<BaseResponse>>
{
    public async Task<ApiResponse<BaseResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            Email = request.Email,
            UserName = request.UserName,
            Name = request.Name,
            Password = request.Password,
            Role = Data.Enums.UserRole.Admin,
        };

        await _createUserService.CreateAsync(newUser, request.Password, cancellationToken);
        var claims = await _claimsService.GenerateUserClaimsAsync(newUser);
        await _signInManager.SignInWithClaimsAsync(newUser, isPersistent: true, claims);

        return new(new() { Id = newUser.Id });
    }
}
