using EVM.Data;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.EventTask.Models.Requests;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace EVM.Services.Features.EventTask.Commands;

public class CreateEventTasksCommandHandler
    (ILogger<CreateEventTasksCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<BaseResponse>> Handle(CreateEventTaskRequest request, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Create", "EventTask");

        var userId = _httpContext.User?.GetId()
            ?? throw new UserNotFoundException();

        var eventEntity = await _appDbContext.Events
            .Where(x => x.Id == request.EventId)
            .Include(x => x.EventTasks)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("Event not found.");

        var user = await _appDbContext.Events
            .Where(x => x.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new UserNotFoundException();

        if (user.Role != Data.Enums.UserRole.Organizer)
            return new(HttpStatusCode.Conflict, "You can`t create tasks");

        var newEventTasks = new Data.Models.EventFeature.EventTask
        {
            EventId = request.EventId,
            Title = request.Title,
            Description = request.Description,
            UserId = userId,
            Status = request.Status ?? Data.Enums.TaskStatus.NotStarted,
        };

        eventEntity.EventTasks.Add(newEventTasks);
        _appDbContext.EventTasks.Add(newEventTasks);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Event task created successfully with ID: {EventTasksId}", newEventTasks.Id);
        return new(new() { Id = newEventTasks.Id });
    }
}
