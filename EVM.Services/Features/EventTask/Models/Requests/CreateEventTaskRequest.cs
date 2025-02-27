namespace EVM.Services.Features.EventTask.Models.Requests;

public record CreateEventTaskRequest
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required Guid EventId { get; set; }

    public Data.Enums.TaskStatus? Status { get; set; }
}
