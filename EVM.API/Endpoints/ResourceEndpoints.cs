using EVM.Services.Features.Resourse.Commands;
using EVM.Services.Features.Resourse.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EVM.API.Endpoints;

public class ResourceEndpoints
{
    private static readonly string Tag = "Resourse";

    public static void Register(WebApplication app)
    {
        app.MapPost(Routes.Resource.Base,
            ([FromServices] CreateResourceCommandHandler commandHandler,
            [FromBody] CreateResourceRequest request,
            CancellationToken cancellationToken) => commandHandler.Handle(request, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);
    }
}
