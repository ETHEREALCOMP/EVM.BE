using EVM.Data;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EVM.Services.Features.Event.Commands;

public class UpdateEventCommandHandler
    (ILogger<UpdateEventCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<BaseResponse>> Handle(Guid eventId, UpdateEventRequest request, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Update", "Event");

        var userId = _httpContext.User?.GetId()
            ?? throw new UserNotFoundException();

        var existingEvent = await _appDbContext.Events
            .Where(x => x.Id == eventId &&
            x.UserId == userId && 
            x.Role == Data.Enums.UserRole.Organizer)
            .AsTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (existingEvent is null)
        {
            _logger.LogWarning("Event with ID: {EventId} for User: {UserId} not found or insufficient permissions.", eventId, userId);
            throw new BaseCustomException("Event not found or you don't have permission to update it!", HttpStatusCode.NotFound);
        }

        var updatedCount = await _appDbContext.Events
        .Where(e => e.Id == eventId && e.UserId == userId)
        .ExecuteUpdateAsync(
            setters => setters
            .SetProperty(e => e.Title, request.Title ?? existingEvent.Title)
            .SetProperty(e => e.Description, request.Description)
            .SetProperty(e => e.Location, request.Location ?? existingEvent.Location)
            .SetProperty(e => e.CreatedOn, DateTime.UtcNow),
            cancellationToken);

        if (updatedCount == 0)
        {
            _logger.LogError("Failed to update event with ID: {EventId}.", eventId);
            throw new BaseCustomException("Couldn't update the data in the database. Please try again later!", HttpStatusCode.InternalServerError);
        }

        _logger.LogInformation("Event with ID: {EventId} was updated successfully!", eventId);
        return new(new() { Id = eventId });
    }
}
