namespace EVM.Services.Features.Event.Models.Requests;

public record UpdateEventRequest
{

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required string Location { get; set; }
}
