using EVM.Services.Features.Models.Responses;
using MediatR;

namespace EVM.Services.Features.Event.Models.Requests;

public record UpdateEventRequest : IRequest<ApiResponse<BaseResponse>>
{
    public required Guid EventId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required string Location { get; set; }
}
