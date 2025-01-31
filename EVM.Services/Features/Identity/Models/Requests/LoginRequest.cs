using EVM.Services.Features.Identity.Models.Responses;
using EVM.Services.Features.Models.Responses;
using MediatR;

namespace EVM.Services.Features.Identity.Models.Requests;

public record LoginRequest : IRequest<ApiResponse<BaseAuthResponse>>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
