using EVM.Services.Features.Resourse.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services.Features.Resourse;

public class ResourceModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CreateResourceCommandHandler>();
    }
}
