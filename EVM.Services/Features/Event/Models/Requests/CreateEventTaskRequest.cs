namespace EVM.Services.Features.Event.Models.Requests;

public record CreateEventTaskRequest
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required Guid EventId { get; set; }
}
