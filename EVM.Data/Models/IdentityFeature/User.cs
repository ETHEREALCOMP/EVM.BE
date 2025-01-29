using EVM.Data.Enums;
using EVM.Data.Models.EventFeature;
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

    public ICollection<Event> Events { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Role).IsRequired();
            entity.HasMany(e => e.RefreshTokens).WithOne(rt => rt.Owner).HasForeignKey(rt => rt.OwnerId);
            entity.HasMany(e => e.Tickets).WithOne(t => t.User).HasForeignKey(t => t.UserId);
            entity.HasMany(e => e.Events).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        });
    }
}
