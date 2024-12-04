using EVM.Data.Enums;
using EVM.Data.Models.PaymentsFeature;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.IdentityFeature;

public class User : IdentityUser<Guid>, IDBConfigurableModel
{
    public required string Name { get; set; }

    public virtual List<RefreshToken> RefreshTokens { get; set; } = [];

    public string? PaymentUserId { get; set; }

    public virtual List<Subscription> Subscriptions { get; set; } = [];

    public virtual List<Invoice> Invoices { get; set; } = [];

    public virtual List<ProjectUser> Projects { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<User>()
            .ToTable(nameof(User));

        builder.Entity<User>()
            .HasKey(x => x.Id);
    }
}