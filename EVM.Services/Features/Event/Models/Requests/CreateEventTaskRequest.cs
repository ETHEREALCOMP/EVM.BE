using EVM.Services.Features.Models.Responses;
using MediatR;

namespace EVM.Services.Features.Event.Models.Requests;

public record CreateEventTaskRequest : IRequest<ApiResponse<BaseResponse>>
{
    public required Guid UserId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required Guid EventId { get; set; }
}
