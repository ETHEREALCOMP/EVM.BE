using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.EventFeature;

public class Coupon : IDBConfigurableModel
{
    public string? Id { get; set; }

    public required string TicketTypesId { get; set; }

    public virtual TicketTypes? TicketTypes { get; set; }

    public required float PercentOfDiscount { get; set; }

    public required decimal Amount { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Coupon>()
            .ToTable(nameof(Coupon));

        builder.Entity<Coupon>()
        .HasOne(x => x.TicketTypes)
        .WithMany(x => x.Coupons)
        .HasForeignKey(x => x.TicketTypesId);

    }

}
