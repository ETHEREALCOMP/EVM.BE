using EVM.Data.Models;
using EVM.Data.Models.EventFeature;
using EVM.Data.Models.IdentityFeature;
using EVM.Data.Models.PaymentsFeature;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EVM.Data;

public class AppDbContext(DbContextOptions<AppDbContext> _options)
    : IdentityDbContext<User, Role, Guid>(_options)
{
    public DbSet<Subscription> Subscriptions { get; set; }

    public DbSet<SubscriptionItem> SubscriptionItems { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<ProjectUser> ProjectUsers { get; set; }

    public DbSet<Coupon> Coupons { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Ticket> Ticket { get; set; }

    public DbSet<TicketTypes> TicketTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        MapIdentity(builder);
        CallModelBuilders(builder);
    }

    private static void MapIdentity(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
    }

    private void CallModelBuilders(ModelBuilder builder)
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var implementations = types.Where(t => typeof(IDBConfigurableModel).IsAssignableFrom(t) && t.IsClass);

        foreach (var implementation in implementations)
        {
            var methodInfo = implementation.GetMethod(nameof(IDBConfigurableModel.BuildModel), BindingFlags.Static | BindingFlags.Public);

            methodInfo?.Invoke(null, [builder]);
        }
    }
}
