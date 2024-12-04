using EVM.Data;
using EVM.Data.Models.IdentityFeature;
using EVM.Services.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EVM.Services.Features.Identity;

public static class IdentityModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        AddDbIdentity(services);
        AddAuthentication(services, configuration);
        //AddAuthorization(services);

        RegisterCommands(services);
        RegisterQueries(services);
        RegisterServices(services);

        //services.AddScoped<RoleSeeder>();

        //services.AddScoped<IGoogleAuthClient, GoogleAuthClient>();
        //services.Configure<GoogleAuthenticationOptions>(configuration.GetSection(GoogleAuthenticationOptions.Path));

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Path));
    }

    private static IServiceCollection RegisterCommands(IServiceCollection services)
    {
        //services.AddScoped<SigninCommand>()
        //    .AddScoped<SignupCommand>()
        //    .AddScoped<SignoutCommand>()
        //    .AddScoped<ForgotPasswordCommand>()
        //    .AddScoped<ResetPasswordCommand>();

        return services;
    }

    private static IServiceCollection RegisterQueries(IServiceCollection services)
    {
        //services.AddScoped<GetCurrentIdentityQuery>();
        return services;
    }

    private static IServiceCollection RegisterServices(IServiceCollection services)
    {
        //services.AddScoped<CreateUserService>();
        //services.AddScoped<TokenService>();
        return services;
    }

    private static IServiceCollection AddDbIdentity(IServiceCollection services)
    {
        services.AddIdentity<User, Role>((options) =>
            {
                options.User.RequireUniqueEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                //options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<User>>()
            .AddUserManager<UserManager<User>>()
            .AddRoleManager<RoleManager<Role>>();

        return services;
    }

    //private static IServiceCollection AddAuthorization(IServiceCollection services)
    //{
    //    services.AddAuthorizationBuilder()
    //        .AddPolicy(Policies.Write, policy =>
    //        {
    //            policy.RequireAuthenticatedUser();
    //            policy.Requirements.Add(new RoleRequirement(UserRole.Write));
    //        })
    //        .AddPolicy(Policies.Read, policy =>
    //        {
    //            policy.RequireAuthenticatedUser();
    //            policy.Requirements.Add(new RoleRequirement(UserRole.Read));
    //        });

    //    return services;
    //}

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

        //services.AddOptionsWithValidateOnStart<GoogleAuthenticationOptions>();

        var jwtSettings = configuration.GetRequiredSection(JwtOptions.Path);
        var key = Encoding.UTF8.GetBytes(jwtSettings.GetRequiredValue(nameof(JwtOptions.Key)));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetRequiredValue(nameof(JwtOptions.Issuer)),
                ValidAudience = jwtSettings.GetRequiredValue(nameof(JwtOptions.Audience)),
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
        });

        return services;
    }
}
