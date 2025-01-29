using EVM.Services.Features.Event;
using EVM.Services.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services;

public static class ServicesModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<JwtService>();
        services.AddScoped<CustomClaimsValidator>();
        EventModule.Register(services);
    }
}
