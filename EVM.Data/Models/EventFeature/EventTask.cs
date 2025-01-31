using EVM.Data.Models.IdentityFeature;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.EventFeature;

public class EventTask : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required Guid UserId { get; set; }

    public User? User { get; set; }

    public required Enums.TaskStatus Status { get; set; }

    public required Guid EventId { get; set; }

    public Event? Event { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<EventTask>()
            .HasKey(t => t.Id);

        builder.Entity<EventTask>()
            .HasOne(t => t.Event)
            .WithMany(e => e.EventTasks)
            .HasForeignKey(t => t.EventId);
    }
}
