using EVM.Data;
using EVM.Data.Enums;
using EVM.Data.Models.IdentityFeature;
using EVM.Services.Features.Identity.Commands;
using EVM.Services.Features.Identity.Models.Const;
using EVM.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services.Features.Identity;

public class IdentityModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        RegisterCommands(services);
        RegisterServices(services);
        AddDbIdentity(services);
    }

    private static IServiceCollection RegisterCommands(IServiceCollection services)
    {
        services.AddScoped<RegisterCommandHandler>();
        services.AddScoped<LoginCommandHandler>();

        return services;
    }

    private static IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.AddScoped<CreateUserService>();
        services.AddScoped<ClaimsService>();
        return services;
    }

    private static IServiceCollection AddDbIdentity(IServiceCollection services)
    {
        services.AddIdentity<User, Role>((options) =>
        {
            options.User.RequireUniqueEmail = true;
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<User>>()
            .AddUserManager<UserManager<User>>()
            .AddRoleManager<RoleManager<Role>>();

        return services;
    }
}
