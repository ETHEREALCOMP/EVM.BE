using EVM.Services.Features.Models.Responses;

namespace EVM.Services.Features.Identity.Models.Responses;

public class BaseAuthResponse: BaseResponse
{
    public required string Token { get; set; }
}
