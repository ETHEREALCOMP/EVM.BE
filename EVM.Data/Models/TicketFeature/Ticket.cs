using EVM.Data.Enums;
using EVM.Data.Models.EventFeature;
using EVM.Data.Models.IdentityFeature;

namespace EVM.Data.Models.TicketFeature;

public class Ticket
{
    public Guid Id { get; set; }

    public required decimal Price { get; set; }

    public required Guid EventId { get; set; }

    public Event? Event { get; set; }

    public required Guid UserId { get; set; }

    public User? User { get; set; }

    public required TicketType Type { get; set; }
}
