using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models.IdentityFeature;

public class Role : IdentityRole<Guid>, IDBConfigurableModel
{
    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Role>()
            .ToTable(nameof(Role));
    }
}