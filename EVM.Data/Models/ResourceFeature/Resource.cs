using EVM.Data.Models.EventFeature;
using System.Security.AccessControl;

namespace EVM.Data.Models.ResourceFeature;

public class Resource
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required ResourceType Type { get; set; }

    public required Guid EventId { get; set; }

    public Event? Event { get; set; }
}
