using EVM.Services.Features.Resourse.Commands;
using EVM.Services.Features.Resourse.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services.Features.Resourse;

public class ResourceModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CreateResourceCommandHandler>();
        services.AddScoped<GetByIdResourceQueryHandler>();
        services.AddScoped<GetResourceQueryHandler>();
        services.AddScoped<DeleteResourcesCommandHandler>();
        services.AddScoped<UpdateResourceCommandHandler>();
    }
}
