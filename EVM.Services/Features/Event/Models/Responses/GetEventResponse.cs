using EVM.Data.Models.EventFeature;

namespace EVM.Services.Features.Event.Models.Responses;

public class GetEventResponse
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required List<EventTask> ETask { get; set; }
}
