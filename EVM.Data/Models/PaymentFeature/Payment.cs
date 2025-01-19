using EVM.Data.Enums;
using EVM.Data.Models.EventFeature;
using EVM.Data.Models.IdentityFeature;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.PaymentFeature;

public class Payment : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required decimal Amount { get; set; }

    public required PaymentStatus Status { get; set; }

    public required Guid EventId { get; set; }

    public Event? Event { get; set; }

    public required Guid UserId { get; set; }

    public User? User { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Payment>()
            .HasKey(p => p.Id);

        builder.Entity<Payment>()
            .HasOne(p => p.Event)
            .WithMany()
            .HasForeignKey(p => p.EventId);

        builder.Entity<Payment>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);
    }
}
