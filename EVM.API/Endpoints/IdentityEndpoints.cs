using EVM.Services.Features.Event.Commands;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Identity.Commands;
using EVM.Services.Features.Identity.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EVM.API.Endpoints;

public static class IdentityEndpoints
{
    private static readonly string Tag = "Identity";

    public static void Register(WebApplication app)
    {
        app.MapPost(Routes.Identity.Signin,
            ([FromServices] LoginCommandHandler commandHandler,
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken) => commandHandler.Handle(request, cancellationToken))
            .AllowAnonymous()
            .WithTags(Tag);

        app.MapPost(Routes.Identity.Signup,
           ([FromServices] RegisterCommandHandler commandHandler,
           [FromBody] RegisterRequest request,
           CancellationToken cancellationToken) => commandHandler.Handle(request, cancellationToken))
           .AllowAnonymous()
           .WithTags(Tag);
    }
}
