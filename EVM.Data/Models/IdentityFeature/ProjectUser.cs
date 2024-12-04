using EVM.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.IdentityFeature;

public class ProjectUser : IDBConfigurableModel
{
    public required Guid CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public required Guid UserId { get; set; }

    public virtual User? User { get; set; }

    public required Guid ProjectId { get; set; }

    public required UserRole Role { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<ProjectUser>()
            .HasKey(x => new { x.UserId, x.ProjectId });

        builder.Entity<ProjectUser>()
            .HasOne(x => x.User)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.UserId);
    }
}
