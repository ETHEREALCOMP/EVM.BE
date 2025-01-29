using EVM.Data;
using EVM.Data.Models.IdentityFeature;
using EVM.Services.Features.Identity.Models.Requests;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Service;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EVM.Services.Features.Identity.Commands;

public class RegisterCommand(CreateUserService _createUserService, AppDbContext _appDbContext) : IRequestHandler<RegisterRequest, ApiResponse<BaseResponse>>
{
    public async Task<ApiResponse<BaseResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            UserName = request.UserName,
            Name = request.Name,
            Password = request.Password,
            Role = Data.Enums.UserRole.None,
        };

        await _createUserService.CreateAsync(user, request.Password, cancellationToken);

        return new(new() { Id = user.Id });
    }
}
