namespace EVM.Services.Features.Event.Models.Requests;

public class CreateEventTaskRequest
{
    public string? Description { get; set; }

    public required TaskStatus Status { get; set; }

    public required Guid EventId { get; set; }
}
