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

        var user = await _appDbContext.Users
               .Where(x => x.Id == userId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new UserNotFoundException();

        if (user.Role != Data.Enums.UserRole.Organizer)
        {
            user.Role = Data.Enums.UserRole.Organizer;
        }

        var existingEvent = _appDbContext.Events.AsTracking().FirstOrDefault(x => x.Id == eventId);

        if (existingEvent == null)
        {
            throw new BaseCustomException("Event was not found!", HttpStatusCode.NotFound);
        }

        existingEvent.Title = request.Title ?? " ";
        existingEvent.Description = request.Description;
        existingEvent.Location = request.Location ?? " ";
        existingEvent.CreatedOn = DateTime.UtcNow;
        existingEvent.UserId = userId;
        existingEvent.Role = user.Role;

        await _appDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation($"Event with ID: {eventId} was updated successfully!", existingEvent.Id);
        return new(new() { Id = existingEvent.Id });
    }
}
