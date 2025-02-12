using EVM.Data.Enums;
using EVM.Data.Models.ResourceFeature;

namespace EVM.Services.Features.Resourse.Models.Responses;

public class GetResourceResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required ResourceType Type { get; set; }

    public required ICollection<EventResource> Resources { get; set; }

    public static explicit operator GetResourceResponse(Resource resource)
    {
        return new GetResourceResponse
        {
            Id = resource.Id,
            Name = resource.Name,
            Type = resource.Type,
            Resources = resource.EventResources.ToList(),
        };
    }
}
