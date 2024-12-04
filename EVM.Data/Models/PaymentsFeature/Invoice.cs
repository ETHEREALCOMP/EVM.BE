using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.PaymentsFeature;

public class Invoice : IDBConfigurableModel
{
    public required string Id { get; set; }

    public string? PaymentUserId { get; set; }

    public required string SubscriptionId { get; set; }

    public virtual Subscription? Subscription { get; set; }

    public decimal AmountPaid { get; set; }

    public required string Currency { get; set; }

    public required string PaymentStatus { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Invoice>()
            .ToTable(nameof(Invoice));

        builder.Entity<Invoice>()
            .HasOne(x => x.Subscription)
            .WithMany(x => x.Invoices)
            .HasForeignKey(x => x.SubscriptionId);

        builder.Entity<Invoice>()
            .Property(x => x.AmountPaid)
            .HasPrecision(8, 2);
    }
}