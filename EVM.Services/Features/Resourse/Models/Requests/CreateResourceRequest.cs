namespace EVM.Services.Features.Resourse.Models.Requests;

public record CreateResourceRequest
{
    public required List<ResourceRequest> Resources { get; set; }

    public required Guid EventId { get; set; }
}
