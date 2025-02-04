using EVM.Data;
using EVM.Data.Models.ResourceFeature;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Features.Resourse.Models.Requests;
using EVM.Services.Features.Resourse.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EVM.Services.Features.Resourse.Commands;

public class CreateResourceCommandHandler
    (ILogger<CreateResourceCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor)
    : IRequestHandler<CreateResourceRequest, ApiResponse<CreateResourcesResponse>>
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<CreateResourcesResponse>> Handle(CreateResourceRequest request, CancellationToken cancellationToken)
    {

        var userId = _httpContext.User?.GetId()
            ?? throw new UserNotFoundException();

        var resources = request.Resources.Select(x => (Resource)x).ToList();

        var eventResources = resources.Select(x => new EventResource
        {
            UserId = userId,
            EventId = request.EventId,
            ResourceId = x.Id,
        }).ToList();

        _appDbContext.Resources.AddRange(resources);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return new(new() { Ids = resources.Select(x => x.Id).ToList() });
    }
}
