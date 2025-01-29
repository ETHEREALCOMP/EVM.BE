using EVM.Data.Models.IdentityFeature;
using EVM.Data;
using EVM.Services.Features.Event;
using EVM.Services.Features.Identity.Commands;
using EVM.Services.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EVM.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using EVM.Services.Features.Identity.Models.Const;
using Microsoft.AspNetCore.Http;

namespace EVM.Services.Features.Identity;

public class IdentityModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        RegisterCommands(services);
        RegisterServices(services);
        AddDbIdentity(services);
        AddAuthorization(services);
        AddAuthentication(services, configuration);
    }

    private static IServiceCollection RegisterCommands(IServiceCollection services)
    {
        services.AddScoped<RegisterCommand>();
        services.AddScoped<LoginCommand>();

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

    private static IServiceCollection AddAuthorization(IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(Policies.Organizer, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new RoleRequirement(UserRole.Organizer));
            })
            .AddPolicy(Policies.Guest, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new RoleRequirement(UserRole.Guest));
            })
            .AddPolicy(Policies.Full, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new RoleRequirement(UserRole.Admin));
            });

        services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();

        return services;
    }

    private static IServiceCollection AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IdentityOptions>(options =>
        {
            if (configuration.GetSection("Environment").GetValue("Local", false))
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
            }
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = AuthSettings.CookieName;
            options.Cookie.HttpOnly = false;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
            options.ExpireTimeSpan = AuthSettings.CookiesExpiration;
            options.SlidingExpiration = true;
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
            options.Events.OnValidatePrincipal = async context =>
            {
                var claimsValidator = context.HttpContext.RequestServices.GetRequiredService<CustomClaimsValidator>();
                await claimsValidator.ValidatePrincipal(context);
            };
        });

        return services;
    }
}
