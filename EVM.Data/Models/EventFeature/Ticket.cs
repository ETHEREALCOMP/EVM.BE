using EVM.Data.Models.PaymentsFeature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Data.Models.EventFeature;

public class Ticket : IDBConfigurableModel
{
    public string? Id { get; set; }

    public required string EventId { get; set; }

    public virtual Event? Event { get; set; }

    public required string TicketTypesId { get; set; }

    public virtual TicketTypes? TicketTypes { get; set; }

    public required string BuyerId { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? CouponId { get; set; }

    public virtual Coupon? Coupon { get; set; }


    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Ticket>()
            .ToTable(nameof(Ticket));

        builder.Entity<Ticket>()
        .HasOne(x => x.Event)
        .WithMany(x => x.Tickets)
        .HasForeignKey(x => x.EventId);

        builder.Entity<Ticket>()
        .HasOne(x => x.Coupon)
        .WithOne()
        .HasForeignKey<Ticket>(x => x.CouponId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
