using EVM.Data.Enums;
using EVM.Data.Models.IdentityFeature;
using EVM.Data.Models.ResourceFeature;
using EVM.Data.Models.TicketFeature;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.EventFeature;

public class Event : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? FinishedOn { get; set; }

    public required string Location { get; set; } // later add Map for localization

    public required Guid UserId { get; set; }

    public virtual User? User { get; set; }

    public required UserRole Role { get; set; }

    public virtual List<EventResource> EventResources { get; set; } = [];

    public virtual List<Ticket> Tickets { get; set; } = [];

    public virtual List<EventTask> EventTasks { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Event>()
                 .HasKey(e => e.Id);

        builder.Entity<Event>()
            .HasOne(e => e.User)
            .WithMany(o => o.Events)
            .HasForeignKey(e => e.UserId);

        builder.Entity<Event>()
            .HasMany(e => e.Tickets)
            .WithOne(t => t.Event)
            .HasForeignKey(t => t.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Event>()
            .HasMany(e => e.EventResources)
            .WithOne(er => er.Event)
            .HasForeignKey(er => er.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Event>()
            .HasMany(e => e.EventTasks)
            .WithOne(t => t.Event)
            .HasForeignKey(t => t.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
