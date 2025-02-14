using EVM.Data.Enums;
using EVM.Data.Models.EventFeature;
using EVM.Data.Models.IdentityFeature;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.ResourceFeature;

public class EventResource : IDBConfigurableModel
{
    public required Guid UserId { get; set; }

    public virtual User? User { get; set; }

    public required UserRole Role { get; set; }

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
