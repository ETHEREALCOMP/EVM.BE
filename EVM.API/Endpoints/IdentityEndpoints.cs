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
            ([FromServices] LoginCommand command,
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken) => command.Handle(request, cancellationToken))
            .AllowAnonymous()
            .WithTags(Tag);

        app.MapPost(Routes.Identity.Signup,
           ([FromServices] RegisterCommand command,
           [FromBody] RegisterRequest request,
           CancellationToken cancellationToken) => command.Handle(request, cancellationToken))
           .AllowAnonymous()
           .WithTags(Tag);
    }
}
