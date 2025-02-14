using EVM.Data;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EVM.Services.Features.Event.Commands;

public class DeleteEventCommandHandler
    (ILogger<DeleteEventCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse> Handle(Guid eventId, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Read", "Event");

        var userId = _httpContext.User?.GetId()
            ?? throw new UserNotFoundException();

        var deletedCount = await _appDbContext.Events
            .Where(x => x.Id == eventId && x.UserId == userId && x.Role == Data.Enums.UserRole.Organizer)
            .ExecuteDeleteAsync(cancellationToken);

        if (deletedCount == 0)
        {
            _logger.LogWarning("Failed to delete event with ID: {EventId}. Event not found or insufficient permissions.", eventId);
            throw new BaseCustomException("Event not found or you don't have permission to delete it!", HttpStatusCode.NotFound);
        }

        _logger.LogInformation("Event with ID: {EventId} was deleted successfully!", eventId);
        return new(HttpStatusCode.OK, "Event was deleted successfully.");
    }
}
