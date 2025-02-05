using EVM.Services.Features.Event;
using EVM.Services.Features.Resourse;
using EVM.Services.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services;

public static class ServicesModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ClaimsService>();
        services.AddScoped<CreateUserService>();
        services.AddScoped<JwtService>();
        EventModule.Register(services);
        ResourceModule.Register(services, configuration);
    }
}
