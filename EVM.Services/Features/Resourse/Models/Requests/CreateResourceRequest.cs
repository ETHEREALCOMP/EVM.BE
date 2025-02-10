namespace EVM.Services.Features.Resourse.Models.Requests;

public record CreateResourceRequest
{
    public required List<CreateResource> Resources { get; set; }

    public required Guid EventId { get; set; }
}
