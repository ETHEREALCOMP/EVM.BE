using EVM.Data;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EVM.Services.Features.Resourse.Commands;

public class DeleteResourcesCommandHandler(ILogger<CreateResourceCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse> Handle(Guid resourceId, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Create", "Resource");

        var userId = _httpContext.User?.GetId() ?? throw new UserNotFoundException();

        var deletedCount = await _appDbContext.EventResources
            .Where(x => x.ResourceId == resourceId && x.UserId == userId && x.Role == Data.Enums.UserRole.Organizer)
            .ExecuteDeleteAsync(cancellationToken);

        if (deletedCount == 0)
        {
            return new ApiResponse(HttpStatusCode.NotFound, "Resource not found or you don't have permission to delete it.");
        }

        await _appDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Resource with ID: {ResourceId} was deleted successfully!", resourceId);
        return new(HttpStatusCode.OK, "Event was deleted successfully.");
    }
}
