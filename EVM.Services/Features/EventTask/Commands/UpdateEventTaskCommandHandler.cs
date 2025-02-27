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

public class UpdateEventTaskCommandHandler(ILogger<CreateEventTasksCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<BaseResponse>> Handle(Guid id, UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Create", "EventTask");

        var userId = _httpContext.User?.GetId()
            ?? throw new UserNotFoundException();

        var existTask = await _appDbContext.EventTasks
            .Where(x => x.Id == id)
            .Include(x => x.Event)
            .AsTracking()
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("Event not found.");

        if (existTask.Event == null || existTask.Event.UserId != userId)
            return new(HttpStatusCode.Forbidden, "You don’t have permission to update this task");

        existTask.Title = request.Title ?? " ";
        existTask.Status = (Data.Enums.TaskStatus)request.Status;
        existTask.Description = request.Description ?? " ";

        await _appDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Event task update successfully with ID: {EventTasksId}", existTask.Id);
        return new(new() { Id = existTask.Id });
    }
}
