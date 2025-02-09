using EVM.Data;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EVM.Services.Features.Event.Commands;

public class CreateEventCommandHandler
    (ILogger<CreateEventCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<BaseResponse>> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Create", "Event");

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

        var newEvent = new Data.Models.EventFeature.Event
        {
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            CreatedOn = DateTime.UtcNow,
            UserId = userId,
            Role = user.Role,
        };

        _appDbContext.Events.Add(newEvent);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Event created successfully with ID: {EventId}", newEvent.Id);
        return new(new() { Id = newEvent.Id });
    }
}

