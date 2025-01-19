using EVM.Data.Enums;
using EVM.Data.Models.EventFeature;
using EVM.Data.Models.IdentityFeature;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.TicketFeature;

public class Ticket : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required decimal Price { get; set; }

    public required Guid EventId { get; set; }

    public Event? Event { get; set; }

    public required Guid UserId { get; set; }

    public User? User { get; set; }

    public required TicketType Type { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Ticket>()
            .HasKey(t => t.Id);

        builder.Entity<Ticket>()
            .HasOne(t => t.Event)
            .WithMany(e => e.Tickets)
            .HasForeignKey(t => t.EventId);

        builder.Entity<Ticket>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserId);
    }
}
