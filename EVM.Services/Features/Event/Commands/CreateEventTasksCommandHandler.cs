using EVM.Data;
using EVM.Data.Enums;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;

namespace EVM.Services.Features.Event.Commands;

public class CreateEventTasksCommandHandler
    (ILogger<CreateEventTasksCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor)
    : IRequestHandler<CreateEventTaskRequest, ApiResponse<BaseResponse>>
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<BaseResponse>> Handle(CreateEventTaskRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.User.GetId() ?? throw new UserNotFoundException();

        var eventEntity = await _appDbContext.Events
        .Include(e => e.EventTasks)
        .FirstOrDefaultAsync(e => e.Id == request.EventId, cancellationToken)
        ?? throw new NotFoundException("Event not found.");

        var newEventTasks = new Data.Models.EventFeature.EventTask
        {
            EventId = request.EventId,
            Title = request.Title,
            Description = request.Description,
            UserId = userId,
            Status = Data.Enums.TaskStatus.NotStarted,
        };

        _appDbContext.EventTasks.Add(newEventTasks);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return new(new() { Id = newEventTasks.Id });
    }
}
