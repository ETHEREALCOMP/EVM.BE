using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.PaymentsFeature;

public class Payment : IDBConfigurableModel
{
    public string? Id { get; set; }

    public required string PaymentUserId { get; set; }

    public decimal Amount { get; set; }

    public required string Currency { get; set; }

    public required string Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Payment>()
            .ToTable(nameof(Payment));
    }
}
