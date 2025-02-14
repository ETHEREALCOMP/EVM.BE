using EVM.Data.Enums;
using EVM.Data.Models.ResourceFeature;

namespace EVM.Services.Features.Resourse.Models.Requests;

public class ResourceRequest
{
    public required string Name { get; set; }

    public required ResourceType Type { get; set; }

    public static implicit operator Resource(ResourceRequest createResourse)
    {
        return new Resource
        {
            Name = createResourse.Name,
            Type = createResourse.Type,
        };
    }
}
