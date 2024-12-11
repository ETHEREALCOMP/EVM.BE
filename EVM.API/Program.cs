using EVM.API.Endpoints;
using EVM.API.Extensions;
using EVM.API.Middleware;
using EVM.Data;
using EVM.Services;
using EVM.Services.Features.Identity;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.WithOrigins("https://localhost:3000");
            });
    });

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Add authorization services
builder.Services.AddAuthorization();

Database.Register(builder.Services, builder.Configuration);
IdentityModule.Register(builder.Services, builder.Configuration);

ServicesModule.Register(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors(o =>
{
    o.AllowAnyHeader();
    o.AllowAnyMethod();
    o.WithOrigins("https://localhost:3000", "https://localhost:3001");
    o.AllowCredentials();
    o.SetPreflightMaxAge(TimeSpan.FromDays(1));
});

app.UseHsts();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.EnsureDatabaseCreated();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

EndpointsModule.Register(app);

app.Run();