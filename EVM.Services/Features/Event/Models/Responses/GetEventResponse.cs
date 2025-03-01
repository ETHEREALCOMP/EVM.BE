namespace EVM.Services.Features.Event.Models.Responses;

public class GetEventResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required List<Data.Models.EventFeature.EventTask> ETask { get; set; }
}
