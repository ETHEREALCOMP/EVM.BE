using EVM.Data.Models.EventFeature;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EVM.Data.Models.ResourceFeature;

public class EventResource : IDBConfigurableModel
{
    public required Guid EventId { get; set; }

    public virtual Event? Event { get; set; }

    public required Guid ResourceId { get; set; }

    public virtual Resource? Resource { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<EventResource>()
           .HasKey(er => new { er.EventId, er.ResourceId });
    }
}
