using EVM.Data;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Event.Models.Responses;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EVM.Services.Features.Event.Query;

public class GetAllTaskQueryHandler
    (ILogger<GetAllTaskQueryHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<List<GetTaskResponse>>> Handle(Guid eventId, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Read", "Event");
        var userId = _httpContext.User.GetId() ?? throw new UserNotFoundException();

        var task = await _appDbContext.EventTasks.Where(x => x.UserId == userId && x.EventId == eventId)
            .Select(x => new GetTaskResponse
            {
                TaskId = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken) ?? throw new EntityNotFoundException("Event");

        _logger.LogInformation("Event with ID: {EventId} has been successfully retrieved and sent.", task.Count);
        return new(task);
    }
}
