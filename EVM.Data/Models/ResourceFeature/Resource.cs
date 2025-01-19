using EVM.Data.Models.EventFeature;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace EVM.Data.Models.ResourceFeature;

public class Resource : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required ResourceType Type { get; set; }

    public required Guid EventId { get; set; }

    public Event? Event { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Resource>()
            .HasKey(r => r.Id);

        builder.Entity<Resource>()
            .HasOne(r => r.Event)
            .WithMany(e => e.Resources)
            .HasForeignKey(r => r.EventId);
    }
}
