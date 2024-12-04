using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.PaymentsFeature;

public class SubscriptionItem : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required string SubscriptionId { get; set; }

    public virtual Subscription? Subscription { get; set; }

    public required string ProductId { get; set; }

    public required string PriceId { get; set; }

    public decimal? PriceAmount { get; set; }

    public required string Currency { get; set; }

    public string? RecurringInterval { get; set; }

    public long RecurringIntervalCount { get; set; }

    public long Quantity { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<SubscriptionItem>()
            .ToTable(nameof(SubscriptionItem));

        builder.Entity<SubscriptionItem>()
            .HasOne(x => x.Subscription)
            .WithMany(x => x.SubscriptionItems)
            .HasForeignKey(x => x.SubscriptionId);

        builder.Entity<SubscriptionItem>()
            .Property(x => x.PriceAmount)
            .HasPrecision(8, 2);

        builder.Entity<SubscriptionItem>()
            .HasIndex(x => new { x.PriceId, x.SubscriptionId, x.ProductId })
            .IsUnique();
    }
}