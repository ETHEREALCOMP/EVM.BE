using EVM.Data.Models.PaymentsFeature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Data.Models.EventFeature;

public class TicketTypes : IDBConfigurableModel
{
    public string? Id { get; set; }

    public required string EventId { get; set; }

    public virtual Event? Event { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required decimal Amount { get; set; }

    public required decimal Price { get; set; }

    public required float PercentOfDiscount { get; set; } = 0;

    public required bool Available { get; set; } = true;

    public virtual List<Ticket> Tickets { get; set; } = [];

    public virtual List<Coupon> Coupons { get; set; } = [];


    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<TicketTypes>()
            .ToTable(nameof(TicketTypes));

        builder.Entity<TicketTypes>()
        .HasOne(x => x.Event)
        .WithMany(x => x.TicketTypes)
        .HasForeignKey(x => x.EventId);

        builder.Entity<TicketTypes>()
        .Property(x => x.Price)
        .HasPrecision(8, 2);

    }

}
