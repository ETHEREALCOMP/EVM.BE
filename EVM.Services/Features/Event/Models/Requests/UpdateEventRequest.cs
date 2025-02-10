using EVM.Services.Features.Models.Responses;

namespace EVM.Services.Features.Event.Models.Requests;

public record UpdateEventRequest
{
    public required Guid EventId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required string Location { get; set; }
}
