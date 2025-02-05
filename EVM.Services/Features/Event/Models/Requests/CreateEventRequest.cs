using EVM.Services.Features.Identity.Models.Responses;
using EVM.Services.Features.Models.Responses;
using MediatR;

namespace EVM.Services.Features.Event.Models.Requests;

public record CreateEventRequest : IRequest<ApiResponse<BaseResponse>>
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required string Location { get; set; }
}
