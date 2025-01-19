using EVM.Data.Enums;
using EVM.Data.Models.EventFeature;
using EVM.Data.Models.IdentityFeature;

namespace EVM.Data.Models.PaymentFeature;

public class Payment
{
    public Guid Id { get; set; }

    public required decimal Amount { get; set; }

    public required PaymentStatus Status { get; set; }

    public required Guid EventId { get; set; }

    public Event? Event { get; set; }

    public required Guid UserId { get; set; }

    public User? User { get; set; }
}
