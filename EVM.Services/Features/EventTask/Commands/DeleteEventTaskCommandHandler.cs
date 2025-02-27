using EVM.Data;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace EVM.Services.Features.EventTask.Commands;

public class DeleteEventTaskCommandHandler(ILogger<CreateEventTasksCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<BaseResponse>> Handle(Guid id, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Read", "EventTask");

        var userId = _httpContext.User?.GetId()
            ?? throw new UserNotFoundException();

        var existTask = await _appDbContext.EventTasks
            .Include(x => x.Event)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken) 
            ?? throw new EntryPointNotFoundException("Task not found!");

        if (existTask.Event == null || existTask.Event.UserId != userId)
            return new(HttpStatusCode.Forbidden, "You don’t have permission to delete this task");

        _appDbContext.EventTasks.Remove(existTask);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Event task deleted successfully with ID: {EventTasksId}", existTask.Id);
        return new(HttpStatusCode.OK, "Task was deleted successful");
    }
}
