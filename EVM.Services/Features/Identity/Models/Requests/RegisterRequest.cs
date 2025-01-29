using EVM.Services.Features.Identity.Models.Responses;
using EVM.Services.Features.Models.Responses;
using MediatR;

namespace EVM.Services.Features.Identity.Models.Requests;

public record RegisterRequest(string UserName, string Name, string Email, string Password) : IRequest<ApiResponse<BaseResponse>>;
