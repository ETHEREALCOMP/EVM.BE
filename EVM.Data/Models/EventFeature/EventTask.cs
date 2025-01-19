using EVM.Data.Models.IdentityFeature;

namespace EVM.Data.Models.EventFeature;

public class EventTask
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public required Guid OrganizerId { get; set; }

    public Organizer? Organizer { get; set; }

    public required TaskStatus Status { get; set; }

    public required Guid EventId { get; set; }

    public Event? Event { get; set; }
}
