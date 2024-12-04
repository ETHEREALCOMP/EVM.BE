using EVM.Data;
using Microsoft.EntityFrameworkCore;

namespace EVM.API.Extensions;

public static class IApplicationBuilderExtensions
{
    public static void EnsureDatabaseCreated(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}