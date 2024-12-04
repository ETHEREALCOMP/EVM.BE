using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.PaymentsFeature;

public class Subscription : IDBConfigurableModel
{
    public required string Id { get; set; }

    public string? PaymentUserId { get; set; }

    public required string Status { get; set; }

    public DateTime CurrentPeriodStart { get; set; }

    public DateTime CurrentPeriodEnd { get; set; }

    public bool CancelAtPeriodEnd { get; set; }

    public DateTime? CanceledOn { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? CancelsOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual List<Invoice> Invoices { get; set; } = [];

    public virtual List<SubscriptionItem> SubscriptionItems { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Subscription>()
            .ToTable(nameof(Subscription));

        builder.Entity<Subscription>()
            .HasMany(s => s.SubscriptionItems)
            .WithOne(si => si.Subscription)
            .HasForeignKey(si => si.SubscriptionId);
    }
}