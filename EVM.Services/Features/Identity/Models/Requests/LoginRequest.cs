using EVM.Services.Features.Identity.Models.Responses;
using EVM.Services.Features.Models.Responses;
using MediatR;

namespace EVM.Services.Features.Identity.Models.Requests;

public record LoginRequest(string Email, string Password) : IRequest<ApiResponse<BaseAuthResponse>>;
