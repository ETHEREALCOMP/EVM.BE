using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.IdentityFeature;

public class RefreshToken : IDBConfigurableModel
{
    public required Guid OwnerId { get; set; }

    public required string Token { get; set; }

    public DateTime Expires { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;

    public required DateTime Created { get; set; }

    public string? CreatedByIp { get; set; }

    public DateTime? Revoked { get; set; }

    public string? RevokedByIp { get; set; }

    public string? ReplacedByToken { get; set; }

    public bool IsActive => Revoked == null && !IsExpired;

    public virtual required User Owner { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder
            .Entity<RefreshToken>()
            .ToTable(nameof(RefreshToken));

        builder
            .Entity<RefreshToken>()
            .HasKey(x => new { x.OwnerId, x.Token });

        builder
            .Entity<RefreshToken>()
            .HasOne(x => x.Owner)
            .WithMany(x => x.RefreshTokens);
    }
}