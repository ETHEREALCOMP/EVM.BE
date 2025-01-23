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

    //public required Guid UserId { get; set; }

    //public User? User { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = [];

    public virtual ICollection<Resource> Resources { get; set; } = [];

    public virtual ICollection<EventTask> EventTasks { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Event>()
             .HasKey(e => e.Id);

        //builder.Entity<Event>()
        //    .HasOne(e => e.User)
        //    .WithMany(o => o.Events)
        //    .HasForeignKey(e => e.UserId);

        builder.Entity<Event>()
            .HasMany(e => e.Tickets)
            .WithOne(t => t.Event)
            .HasForeignKey(t => t.EventId);

        builder.Entity<Event>()
            .HasMany(e => e.Resources)
            .WithOne(r => r.Event)
            .HasForeignKey(r => r.EventId);

        builder.Entity<Event>()
            .HasMany(e => e.EventTasks)
            .WithOne(t => t.Event)
            .HasForeignKey(t => t.EventId);
    }
}
