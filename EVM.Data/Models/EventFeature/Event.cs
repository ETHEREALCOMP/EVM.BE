using EVM.Data.Models.IdentityFeature;

namespace EVM.Data.Models.EventFeature;

public class Event
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? FinishedOn { get; set; }

    public required string Location { get; set; } // later add Map for localization

    public required Guid OrganizerId { get; set; }

    public Organizer? Organizer { get; set; }
}
