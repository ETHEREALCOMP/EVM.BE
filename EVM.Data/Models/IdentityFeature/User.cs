using EVM.Data.Enums;
using EVM.Data.Models.TicketFeature;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.IdentityFeature;

public class User : IdentityUser<Guid>, IDBConfigurableModel
{
    public required string Name { get; set; }

    public required string Password { get; set; }

    public required UserRole Role { get; set; }

    public virtual List<RefreshToken> RefreshTokens { get; set; } = [];

    public virtual ICollection<Ticket> Tickets { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<User>()
            .ToTable(nameof(User));

        builder.Entity<User>()
            .HasKey(x => x.Id);

        builder.Entity<User>()
           .HasMany(u => u.Tickets)
           .WithOne(t => t.User)
           .HasForeignKey(t => t.UserId);
    }
}