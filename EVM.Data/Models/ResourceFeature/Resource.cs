using EVM.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.ResourceFeature;

public class Resource : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required ResourceType Type { get; set; }

    public virtual List<EventResource> EventResources { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Resource>()
            .HasKey(r => r.Id);

        builder.Entity<Resource>()
                .HasMany(r => r.EventResources)
                .WithOne(er => er.Resource)
                .HasForeignKey(er => er.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
