using EVM.Data.Models.ResourceFeature;

namespace EVM.Services.Features.Resourse.Models.Responses;

public class GetResourceResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required List<Resource> Resources { get; set; }
}
