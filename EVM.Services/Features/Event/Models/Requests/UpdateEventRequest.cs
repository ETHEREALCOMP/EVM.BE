namespace EVM.Services.Features.Event.Models.Requests;

public record UpdateEventRequest
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }
}
