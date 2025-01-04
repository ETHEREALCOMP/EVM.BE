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
            ([FromServices] SigninCommand command,
            [FromBody] SigninRequest request)
            => command.ExecuteAsync(request))
            .AllowAnonymous()
            .WithTags(Tag);

        app.MapPost(Routes.Identity.Signup,
           ([FromServices] SignupCommand command,
           [FromBody] SignupRequest request,
           CancellationToken cancellationToken)
           => command.ExecuteAsync(request, cancellationToken))
           .AllowAnonymous()
           .WithTags(Tag);

        app.MapPost(Routes.Identity.Signout,
           ([FromServices] SignoutCommand command)
           => command.ExecuteAsync())
           .RequireAuthorization()
           .WithTags(Tag);
    }
}
