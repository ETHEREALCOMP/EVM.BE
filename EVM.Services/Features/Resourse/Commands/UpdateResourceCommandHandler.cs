using EVM.Data;
using EVM.Data.Models.ResourceFeature;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Features.Resourse.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EVM.Services.Features.Resourse.Commands;

public class UpdateResourceCommandHandler
    (ILogger<UpdateResourceCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<BaseResponse>> Handle(Guid resourceId, UpdateResourceRequest request, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Update", "Resource");

        var userId = _httpContext.User?.GetId() ?? throw new UserNotFoundException();

        if (!await IsOrganizerAsync(userId, cancellationToken))
        {
            return new(HttpStatusCode.Forbidden, "Only organizers can update resources.");
        }

        var existingResource = await _appDbContext.Resources
            .Where(x => x.Id == resourceId)
            .Include(x => x.EventResources)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingResource == null)
        {
            return new(HttpStatusCode.NotFound, "Resource not found.");
        }

        existingResource.Name = request.Name ?? existingResource.Name;
        existingResource.Type = request.Type ?? existingResource.Type;

        var updatedEventResources = existingResource.EventResources
            .Select(eventResource =>
            {
                eventResource.Role = Data.Enums.UserRole.Organizer;
                return eventResource;
            })
            .ToList();

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return new(new() { Id = resourceId });
    }

    private async Task<bool> IsOrganizerAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Events
            .AnyAsync(x => x.UserId == userId && x.Role == Data.Enums.UserRole.Organizer, cancellationToken);
    }
}

