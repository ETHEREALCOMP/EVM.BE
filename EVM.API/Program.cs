using EVM.API.Endpoints;
using EVM.API.Extensions;
using EVM.API.Middleware;
using EVM.Data;
using EVM.Services;
using EVM.Services.Features.Identity;
using EVM.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
        policy.WithOrigins("https://localhost:3000", "https://localhost:3001")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateEventPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("role", "Admin", "EventCreator");
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero  // Вимикає затримку валідації
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"Token validated for: {context.Principal.Identity.Name}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CustomClaimsValidator>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;  // Ensures at least one uppercase letter
    options.Password.RequireNonAlphanumeric = true;  // Ensures at least one non-alphanumeric character
    options.Password.RequiredLength = 3;  // Minimum length of the password
});

IdentityModule.Register(builder.Services, builder.Configuration);
Database.Register(builder.Services, builder.Configuration);
ServicesModule.Register(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowLocalhost");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHsts();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.EnsureDatabaseCreated();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
EndpointsModule.Register(app);

app.Run();
