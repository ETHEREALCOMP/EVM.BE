using EVM.Data;
using EVM.Data.Models.ResourceFeature;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Features.Resourse.Models.Requests;
using EVM.Services.Features.Resourse.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EVM.Services.Features.Resourse.Commands;

public class CreateResourceCommandHandler
    (ILogger<CreateResourceCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<CreateResourcesResponse>> Handle(CreateResourceRequest request, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Create", "Resource");

        var userId = _httpContext.User?.GetId() ?? throw new UserNotFoundException();

        var isOrganizer = await _appDbContext.Events
            .Where(x => x.UserId == userId && x.Role == Data.Enums.UserRole.Organizer)
            .AnyAsync(cancellationToken);

        if (!isOrganizer)
        {
            return new(HttpStatusCode.Conflict, "You can`t create resources");
        }

        var resources = request.Resources.Select(r => new Resource
        {
            Name = r.Name,
            Type = r.Type,
        }).ToList();

        var eventResources = resources.Select(r => new EventResource
        {
            UserId = userId,
            EventId = request.EventId,
            ResourceId = r.Id,
            Role = Data.Enums.UserRole.Organizer,
        });

        _appDbContext.Resources.AddRange(resources);
        _appDbContext.EventResources.AddRange(eventResources);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return new(new() { Ids = resources.Select(r => r.Id).ToList() });
    }
}
