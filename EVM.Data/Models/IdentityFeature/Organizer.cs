using EVM.Data.Models.EventFeature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Data.Models.IdentityFeature;

public class Organizer : IDBConfigurableModel
{
    public Guid OrganizerId { get; set; }

    public required string Name { get; set; }

    public required string ContactInfo { get; set; } // later rework to list

    public ICollection<Event> Events { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Organizer>()
           .HasKey(o => o.OrganizerId);

        builder.Entity<Organizer>()
            .HasMany(o => o.Events)
            .WithOne(e => e.Organizer)
            .HasForeignKey(e => e.OrganizerId);
    }
}
